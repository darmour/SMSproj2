

 Configuring the app to use Azure Communication Services
------------------------
In the appsettings.json enter values for the following:
  "EmailConnectionString": obtain this in the keys section of the ACS resource which has your email services in the Azure portal
  "EmailFromAddress": Usually donotreply@<your email domain>
  "SMSConnectionString": obtain this in the keys section of the ACS resource which has your phone number in the Azure portal
  "SMSfromSender": Your phone number in E.164 format