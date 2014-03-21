
#include <Time.h>
#include <LiquidCrystal.h>

//variables
const int sensorPin = A0;
const int lightPin = A1;
int sensorVal = 0;
int lightVal = 0;
double voltage = 0;
double celsius = 0;
int i=0;
LiquidCrystal lcd(12,11,5,4,3,2);
void setup()
{
  //seral opening
  Serial.begin(9600);
  pinMode(10,OUTPUT);
  pinMode(9,OUTPUT);
  lcd.begin(16,2);
  lcd.setCursor(0,1);
  
}

void loop()
{
  if((i%2)==0)
  {
    digitalWrite(9,HIGH);
    lightVal = analogRead(lightPin);
    digitalWrite(9,LOW);
    lcd.setCursor(0,0);
    lcd.print("L val:");
    lcd.print(lightVal);
    if(lightVal<420){digitalWrite(10,HIGH);}
    else{digitalWrite(10,LOW);}
   } 
  //sensor value reding and calculating
  sensorVal = analogRead(sensorPin);
  voltage = (sensorVal/1024.0) * 5.0;
  celsius=(voltage*100)-7.5;
  celsius=(voltage*100);
  Serial.print("  S val:");
  Serial.print(sensorVal);
  Serial.print("  V val:");
  Serial.print(voltage);
  Serial.print("  C val:");
  Serial.print(celsius);
  Serial.print("\n");
  //print on lcd
  
  lcd.setCursor(0,1);
  lcd.print("C val:");
  lcd.print(celsius);
  i++;
  
}
