

 Configuring the app to use Azure Communication Services <BR>
------------------------<BR>
In the appsettings.json file, enter values for the following: <BR>
  "EmailConnectionString": obtain this in the keys section of the ACS resource which has your email services in the Azure portal <BR>
  "EmailFromAddress": Usually donotreply@yourEmailDomain <BR>
  "SMSConnectionString": obtain this in the keys section of the ACS resource which has your phone number in the Azure portal <BR>
  "SMSfromSender": Your phone number in E.164 format <BR>
