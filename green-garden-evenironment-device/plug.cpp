#include "plug.h"
bool plugStatus = false;

Plug::Plug(int plug)
{
    // Use 'this->' to make the difference between the
    // 'pin' attribute of the class and the
    // local variable 'pin' created from the parameter.
    this->plug = plug;
    init();
}
void Plug::init()
{
    pinMode(plug, OUTPUT);
    // Always try to avoid duplicate code.
    // Instead of writing digitalWrite(pin, LOW) here,
    // call the function off() which already does that
    off();
}
void Plug::on()
{
    digitalWrite(plug, HIGH);
    plugStatus = true;
    Serial.println("plug has been turned on");
}
void Plug::off()
{
    digitalWrite(plug, LOW);
    plugStatus = false;
    Serial.println("plug has been turned off");
}

bool Plug::status()
{
    Serial.print("Plug Status: ");
    Serial.println(plugStatus);
    return plugStatus;
}