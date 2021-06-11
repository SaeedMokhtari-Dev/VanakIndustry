using NLog.Layouts;

namespace VanakIndustry.Web.Logging.Services
{
    public static class LogLayoutProvider
    {
        public static JsonLayout DefaultFileLayout()
        {
            var jsonLayout = new JsonLayout {IncludeAllProperties = true};

            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Level", Layout = "${level:upperCase=true}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Date", Layout = @"${date:format=dd MMM yyyy - HH\:mm\:ss}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Message", Layout = "${message}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Exception", Layout = "${exception:format=tostring}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Properties", Layout = "${all-event-properties}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Class", Layout = "${callsite:className=true:methodName=false}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Method", Layout = "${callsite:className=false:methodName=true}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Url", Layout = "${aspnet-request-url}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Action", Layout = "${aspnet-mvc-action}" });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "Logger", Layout = "${logger}" });

            return jsonLayout;
        }
    }
}