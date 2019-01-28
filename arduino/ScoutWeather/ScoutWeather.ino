#include <HTTPClient.h>
#include <Adafruit_BME280.h>
#include <WiFi.h>

const char* ssid = "[YOUR WIFI NAME]";
const char* password =  "[YOUR WIFI PASS]";
const char* apiLocation = "[YOUR API HERE]";
const int readingEverySeconds = 30 * 60; // read every 30 minutes

#define BME280_ADDRESS_FAILOVER 0x76

Adafruit_BME280 bme;  

int iterations = 1800;
int wiFiStatus = WL_IDLE_STATUS;

float temperature = 0;
float humidity = 0;
float pressure = 0;
byte mac[6];

bool bme280Loaded = false;

void setup() {
  Serial.begin(115200);

  initWiFi();
  
  printWiFiStatus();

  initSensor();
}

void loop() {
  printWiFiStatus();

  if(wiFiStatus != WL_CONNECTED){
    initWiFi();
    delay(1000);
    return;
  }
  
  if(!bme280Loaded)
  {
    Serial.println("No BME280 sensor");
    delay(1000);
    return;
  }
    
  getTemperature();
  getHumidity();
  getPressure();
  
  String content = "{ \"mac\": \""+String(mac[5],HEX)+":"+String(mac[4],HEX)+":"+String(mac[3],HEX)+":"+String(mac[2],HEX)+":"+String(mac[1],HEX)+":"+String(mac[0],HEX)+"\", " + 
                  "\"temp\":"+String(temperature,1)+", " + 
                  "\"hum\":"+String(humidity,1)+", " + 
                  "\"pres\":"+String(pressure,1)+" }";
  Serial.println(content);

  if(iterations++ < readingEverySeconds)
  {
    delay(1000);
    return;
  }

  iterations = 0;
  
  HTTPClient http;
  http.begin(apiLocation);
  http.addHeader("Content-Type", "application/json");
  int httpResponseCode = http.POST(content);
  Serial.println("Http Code:"+String(httpResponseCode));
  http.end();
  
  delay(1000);
}

void printWiFiStatus()
{
  wiFiStatus = WiFi.status();

  switch(wiFiStatus) {
    case WL_CONNECTED:
      Serial.println("WF Conn");
      break;
    case WL_NO_SHIELD:
      Serial.println("No WF");
      break;
    case WL_IDLE_STATUS:
      Serial.println("WF Idle");
      break;
    case WL_NO_SSID_AVAIL:
      Serial.println("WF No SSID");
      break;
    case WL_SCAN_COMPLETED:
      Serial.println("WF Scn complete");
      break;
    case WL_CONNECT_FAILED:
      Serial.println("WF Con failed");
      break;
    case WL_CONNECTION_LOST:
      Serial.println("WF Con lost");
      break;
    case WL_DISCONNECTED:
      Serial.println("WF Disc");
      break;
  }
}

void initWiFi()
{ 
  WiFi.enableSTA(true);
  WiFi.begin(ssid, password);
  
  while (WiFi.status() == WL_IDLE_STATUS) {
    delay(1000);
    Serial.println("WF connecting");
  }
  
  WiFi.macAddress(mac);
}

void initSensor()
{
  bme280Loaded = bme.begin(BME280_ADDRESS);
  if (bme280Loaded) {
    return;
  }
  bme280Loaded - bme.begin(BME280_ADDRESS_FAILOVER);
  if (!bme280Loaded) {
    Serial.println("No BME280 sensor, check wiring!");
  }
}

void getTemperature()
{
  temperature = bme.readTemperature();
}

void getHumidity()
{
  humidity = bme.readHumidity();
}

void getPressure()
{
  float reading = bme.readPressure();
  float seaLevel = bme.seaLevelForAltitude(0.0,reading);
  pressure = seaLevel/100.0F;
}