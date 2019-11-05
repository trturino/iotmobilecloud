
#include <WiFi.h>
#include "Esp32MQTTClient.h"
// Please input the SSID and password of WiFi
const char* ssid     = "MySpectrumWiFi31-2G";
const char* password = "mobileturtle769";
const int LED = 32;
static const char* connectionString = "HostName=turino.azure-devices.net;DeviceId=turinodevice;SharedAccessKey=9/nEQgjJgFrmqWnBZDIHW8b3BAdVEKUfjesFT0fzyNI=";
static bool hasIoTHub = false;
static bool isLightOn = false;

void setup() {
  Serial.begin(115200);
  Serial.println("Starting connecting WiFi.");
  delay(10);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  pinMode (LED, OUTPUT);
  if (!Esp32MQTTClient_Init((const uint8_t*)connectionString))
  {
    hasIoTHub = false;
    Serial.println("Initializing IoT hub failed.");
    return;
  }
  Esp32MQTTClient_SetMessageCallback(MessageCallback);
  Serial.println("start receiving messages.");
  hasIoTHub = true;
}
static void MessageCallback(const char* payLoad, int size)
{
  Serial.println("Message callback:");
  Serial.print("'");
  Serial.print(payLoad);
  Serial.println("'");
  
  if (strcmp(payLoad,"1") == 0) { 
    Serial.print("Turning ON!");
    digitalWrite(LED, HIGH);
    isLightOn = true;
  }
  else if (strcmp(payLoad,"0") == 0) {
    Serial.print("Turning OFF!");
    digitalWrite(LED, LOW);
    isLightOn = false;
  }
}
void loop() {
  Serial.println("start sending events.");
  if (hasIoTHub)
  {
    char buff[128];
    snprintf(buff, 128, "{\"topic\":\"iot\"}");
    
    if (Esp32MQTTClient_SendEvent(buff))
    {
      Serial.println("Sending data succeed");
    }
    else
    {
      Serial.println("Failure...");
    }
    delay(1000);
  }
}
