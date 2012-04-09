## Agressive Mailer

You have to add an app.config file that looks something like this:

```
<?xml version="1.0"?>
<configuration>
	<appSettings>
		<add key="email_to" value="xxx"/>
		<add key="email_to_name" value="xxx"/>
		<add key="email_from" value="xxx"/>
		<add key="email_from_name" value="xxx"/>
	</appSettings>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
	</startup>
	<system.net>
		<mailSettings>
			<smtp from="xxx@xxx.com">
				<network host="smtp.gmail.com"
						 port="465"
						 defaultCredentials="false"
						 userName="xxx@xxx.com"
						 password="xxx"
						 enableSsl="true" />
			</smtp>
		</mailSettings>
	</system.net>
</configuration>
```

Then you start the application!