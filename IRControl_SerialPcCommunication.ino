#include <IRremote.h>

#define DIGI_PIN_SOMETHING 3
uint8_t commandIn;
int on=0;
int pass=0;
IRrecv irrecv(11);
decode_results results;

void setup() {
  Serial.begin(57600);
  irrecv.enableIRIn(); // Start the receiver
  pinMode(2,OUTPUT);
  pinMode(3,OUTPUT);
}


void loop() {
   /*---------------------------------------------- if per la recezione di dati via seriale -----------------------------*/
    if (Serial.available() > 0)
    {
        /*------------------------- legge i dati inviati dal pc ----------------------*/
        commandIn = Serial.read();
        //test if the pc sent an 'a' or 'b'
        switch (commandIn)
        {
            case '1':
            {
                //we got an 'a' from the pc so turn on the digital pin
                digitalWrite(DIGI_PIN_SOMETHING,HIGH);
                break;
            }
            case '0':
            {
                //we got an 'b' from the pc so turn off the digital pin
                digitalWrite(DIGI_PIN_SOMETHING,LOW);
                break;
            }
        }
    }
  /*------------------------------------------------- if con filtro ---------------------------------------------------*/
  if (irrecv.decode(&results)) {
    //Serial.println(results.value);
    switch(results.value){
    /*---------------------------------------------- tasto 1 ------------------------------------------*/ 
     case 3024380778:
             if(on==0)
               {
                 digitalWrite(2,HIGH);
                 on=1;
                 Serial.println("1"); 
                 delay(10);
               }
               else
               {
                 digitalWrite(2,LOW);
                 on=0;
                 Serial.println("1"); 
                 delay(10);
               }
         break;
         
    /*---------------------------------------------- tasto 2 ------------------------------------------*/
     case 3674004067:
             if(on==0)
               {
                 digitalWrite(3,HIGH);
                 on=1;
                 Serial.println("2"); 
                 delay(10);
               }
               else
               {
                 digitalWrite(3,LOW);
                 on=0;
                 Serial.println("2"); 
                 delay(10);
               }
         break;
         
    /*---------------------------------------------- tasto destra ------------------------------------------*/
     case 1313865889:
                 Serial.println("Right"); 
         break;
         
    /*---------------------------------------------- tasto destra ------------------------------------------*/
     case 3607392081:
                 Serial.println("Up"); 
         break;
         
    /*---------------------------------------------- tasto destra ------------------------------------------*/
     case 3379257582:
                 Serial.println("Down"); 
         break;   
         
    /*---------------------------------------------- tasto destra ------------------------------------------*/
     case 643505126:
                 Serial.println("Left"); 
         break;
         
    /*------------------------------------------------ tasto ok --------------------------------------------*/
     case 848474797:
                 Serial.println("Click"); 
         break;
         
    /*---------------------------------------------- tasto on/off ------------------------------------------*/     
     case 2152446381:
                 Serial.println("Off"); 
         break;
         
    /*---------------------------------------------- default -------------------------------------------*/
     default:
         break;
         }
    irrecv.resume(); // Receive the next value
  }
  
}

