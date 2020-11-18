
#include <OneWire.h>
#include <DallasTemperature.h>
#include <hd44780.h>
#include <hd44780ioClass/hd44780_pinIO.h>
#include "button.h"

#define TEMPERATURE_PRECISION 9 // Lower resolution

namespace timing
{
    // 1 second
    const long oneSecond = 1000;
    // one minute
    const long oneMinute = oneSecond * 60;
    // 4.5 minutes
    const long fourMinutesThirtySeconds = (oneMinute * 4) + (oneSecond * 30);
    // 60 minutes
    const long oneHour = oneMinute * 60;
    // 10 hours
    const long tenHours = oneHour * 10;
    // 14 hours
    const long fourteenHours = oneHour * 14;

    // pump start millis
    unsigned long pumpMillis = 0;

    // time since last change (light on/off)
    unsigned long lightMillis = 0;

    // Last run
    unsigned long lastRun = 0;

} // namespace timing

namespace pin
{
    const byte ph_sensor = A0;
    const byte tds_sensor = A1;
    const int light = 13;
    const int lcd_rs = 12;
    const int lcd_rw = 11;
    const int lcd_en = 10;
    const int light_button = 9;
    const int ph_button = 8;
    const int pump_button = 7;
    const int one_wire_bus = 6;
    const int lcd_db4 = 5;
    const int lcd_db5 = 4;
    const int lcd_db6 = 3;
    const int lcd_db7 = 2;
    const int pump = 1;
} // namespace pin

namespace device
{
    float aref = 4.3;
}

namespace sensor
{
    float previosEc = 0;
    float ec = 0;
    unsigned int previosTds = 0;
    unsigned int tds = 0;
    float previosWaterTemp = 0;
    float waterTemp = 0;
    float ecCalibration = 1;
    float ph = 0;
    bool lightStatus = false;
    bool pumpStatus = false;
} // namespace sensor

Button lightButton(pin::light_button);
Button pumpButton(pin::pump_button);
Button phButton(pin::ph_button);

hd44780_pinIO lcd(pin::lcd_rs, pin::lcd_rw, pin::lcd_en, pin::lcd_db4, pin::lcd_db5, pin::lcd_db6, pin::lcd_db7);

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(pin::one_wire_bus);

// Pass our oneWire reference to Dallas Temperature.
DallasTemperature sensors(&oneWire);

DeviceAddress waterTemperatureDeviceAddress;

// function to print a device address
void printDeviceAddress(DeviceAddress deviceAddress)
{
    for (uint8_t i = 0; i < 8; i++)
    {
        if (deviceAddress[i] < 16)
            Serial.print(i);
        Serial.print(deviceAddress[i], HEX);
    }
}

void printTemperature(float tempC)
{
    // print to serial
    Serial.print("Temp C: ");
    Serial.print(tempC);
    Serial.print(" Temp F: ");
    Serial.print(DallasTemperature::toFahrenheit(tempC), 2);

    if (sensor::waterTemp != sensor::previosWaterTemp)
    {
        //print to lcd
        lcd.setCursor(8, 1);
        lcd.print("F:");
        lcd.print(DallasTemperature::toFahrenheit(sensor::waterTemp), 2);
    }
}

void setupTemperatureDevices()
{
    // Start up the library
    sensors.begin();

    // report parasite power requirements
    Serial.print("Parasite power is: ");
    if (sensors.isParasitePowerMode())
    {
        Serial.println("ON");
    }
    else
    {
        Serial.println("OFF");
    }

    // Search the wire for address
    if (sensors.getAddress(waterTemperatureDeviceAddress, 0))
    {
        Serial.print("Found device ");
        Serial.print(0, DEC);
        Serial.print(" with address: ");
        printDeviceAddress(waterTemperatureDeviceAddress);
        Serial.println();

        Serial.print("Setting resolution to ");
        Serial.println(TEMPERATURE_PRECISION, DEC);

        // set the resolution to TEMPERATURE_PRECISION bit (Each Dallas/Maxim device is capable of several different resolutions)
        sensors.setResolution(waterTemperatureDeviceAddress, TEMPERATURE_PRECISION);

        Serial.print("Resolution actually set to: ");
        Serial.print(sensors.getResolution(waterTemperatureDeviceAddress), DEC);
        Serial.println();
    }
    else
    {
        Serial.print("Found ghost device at ");
        Serial.print(0, DEC);
        Serial.print(" but could not detect address. Check power and cabling");
    }
}

void checkWaterTemperature()
{
    sensors.requestTemperatures();
    sensor::previosWaterTemp = sensor::waterTemp;
    sensor::waterTemp = sensors.getTempC(waterTemperatureDeviceAddress);
    printTemperature(sensor::waterTemp);
}

void printTDS()
{
    Serial.print("TDS:");
    Serial.println(sensor::tds);
    Serial.print("EC:");
    Serial.println(sensor::ec, 2);

    // set the cursor the fiest line and 1st character
    if (sensor::tds != sensor::previosTds)
    {
        lcd.setCursor(0, 0);
        lcd.print("TDS:");
        lcd.print(sensor::tds);
    }
    if (sensor::ec != sensor::previosEc)
    {
        lcd.setCursor(8, 0);
        lcd.print("EC:");
        lcd.print(sensor::ec, 2);
    }
}

void checkTDS()
{
    sensor::previosEc = sensor::ec;
    sensor::previosTds = sensor::tds;
    float rawEc = analogRead(pin::tds_sensor) * device::aref / 1024.0;                                          // read the analog value more stable by the median filtering algorithm, and convert to voltage value
    float temperatureCoefficient = 1.0 + 0.02 * (sensor::waterTemp - 25.0);                                     // temperature compensation formula: fFinalResult(25^C) = fFinalResult(current)/(1.0+0.02*(fTP-25.0));
    sensor::ec = (rawEc / temperatureCoefficient) * sensor::ecCalibration;                                      // temperature and calibration compensation
    sensor::tds = (133.42 * pow(sensor::ec, 3) - 255.86 * sensor::ec * sensor::ec + 857.39 * sensor::ec) * 0.5; //convert voltage value to tds value
    printTDS();
}

void printPh()
{
    Serial.print("    pH:");
    Serial.print(sensor::ph, 2);
    Serial.println(" ");
    // set the cursor to column 0, line 1
    // (note: line 1 is the second row, since counting begins with 0):
    lcd.setCursor(0, 1);
    // print the number Ph:
    lcd.print("pH:");
    lcd.print(sensor::ph, 2);
}

void checkPh()
{
    unsigned long int avgValue; //Store the average value of the sensor feedback
    float b;
    int buf[10], temp;
    for (int i = 0; i < 10; i++) //Get 10 sample value from the sensor for smooth the value
    {
        buf[i] = analogRead(pin::ph_sensor);
        delay(10);
    }
    for (int i = 0; i < 9; i++) //sort the analog from small to large
    {
        for (int j = i + 1; j < 10; j++)
        {
            if (buf[i] > buf[j])
            {
                temp = buf[i];
                buf[i] = buf[j];
                buf[j] = temp;
            }
        }
    }
    avgValue = 0;
    for (int i = 2; i < 8; i++) //take the average value of 6 center sample
        avgValue += buf[i];
    float phValue = (float)avgValue * 5.0 / 1024 / 6; //convert the analog into millivolt
    sensor::ph = 3.5 * phValue;                       //convert the millivolt into pH value
    printPh();
}

void checkPhButtonPressed()
{
    phButton.update();
    if (phButton.isPressed())
    {
        checkPh();
        lcd.display();
    }
}

void setupLight()
{
    pinMode(pin::light, OUTPUT);
    // Always try to avoid duplicate code.
    // Instead of writing digitalWrite(pin, LOW) here,
    // call the function off() which already does that
    lightOff();
}

void lightOn()
{
    digitalWrite(pin::light, HIGH);
    sensor::lightStatus = true;
    Serial.println("Light has been turned on");
}

void lightOff()
{
    digitalWrite(pin::light, LOW);
    sensor::lightStatus = false;
    Serial.println("Light has been turned off");
}

void checkLightButtonPressed(unsigned long currentMillis)
{
    lightButton.update();
    if (lightButton.isPressed())
    {
        Serial.println("Light Button Pressed");
        if (sensor::lightStatus)
        {
            lightOff();
            timing::lightMillis = currentMillis;
        }
        else if (!sensor::lightStatus)
        {
            lightOn();
            Serial.println("Light turned on.");
            timing::lightMillis = currentMillis;
        }
    }
}

void checkLights(unsigned long currentMillis)
{
    // check to see if the light is on
    if (sensor::lightStatus)
    {
        if (currentMillis - timing::lightMillis >= timing::fourteenHours)
        {
            lightOff();
            timing::lightMillis = currentMillis;
        }
    }
    else if (!sensor::lightStatus)
    {
        if (currentMillis - timing::lightMillis >= timing::tenHours)
        {
            lightOn();
            timing::lightMillis = currentMillis;
        }
    }
}

void setupPump()
{
    pinMode(pin::pump, OUTPUT);
    // Always try to avoid duplicate code.
    // Instead of writing digitalWrite(pin, LOW) here,
    // call the function off() which already does that
    pumpOff();
}

void pumpOn()
{
    digitalWrite(pin::pump, HIGH);
    sensor::pumpStatus = true;
    Serial.println("Pump has been turned on");
}

void pumpOff()
{
    digitalWrite(pin::pump, LOW);
    sensor::pumpStatus = false;
    Serial.println("Pump has been turned off");
}

void checkPumpButtonPressed(unsigned long currentMillis)
{
    pumpButton.update();
    if (pumpButton.isPressed())
    {
        Serial.println("Pump Button Pressed");
        if (sensor::pumpStatus)
        {
            pumpOff();
            timing::pumpMillis = currentMillis;
        }
        else
        {
            pumpOn();
            timing::pumpMillis = currentMillis;
        }
    }
}

void checkPump(unsigned long currentMillis)
{
    // Pump is on
    if (sensor::pumpStatus)
    {
        // make sure the pump only runs for 4.5 minutes
        if (currentMillis - timing::pumpMillis >= timing::fourMinutesThirtySeconds)
        {
            pumpOff();
            timing::pumpMillis = currentMillis;
        }
    }
    // pump is off
    else if (!sensor::pumpStatus)
    {
        if (currentMillis - timing::pumpMillis >= timing::oneHour)
        {
            pumpOn();
            timing::pumpMillis = currentMillis;
        }
    }
}

void setup()
{
    lcd.begin(16, 2);
    // Print a message to the LCD.
    lcd.print("Initializing...");
    Serial.begin(115200);
    while (!Serial)
    {
        ;
    }
    Serial.println("Initializing..."); //Test the serial monitor
    setupTemperatureDevices();
    setupLight();
    lightOn();
    setupPump();
    lcd.clear();
}

int displayCount = 0;
void loop()
{
    unsigned long currentMillis = millis();
    checkLightButtonPressed(currentMillis);
    checkPumpButtonPressed(currentMillis);
    checkPhButtonPressed();
    checkLights(currentMillis);
    checkPump(currentMillis);
    if (currentMillis - timing::lastRun >= timing::oneSecond)
    {
        if (!sensor::pumpStatus)
        {
            checkWaterTemperature();
            checkTDS();
        }
    }
    if(displayCount >= 120){
        lcd.noDisplay();
        lcd.noBacklight();
        displayCount = 0;
    }
    delay(1000);
    displayCount += 1;
}
