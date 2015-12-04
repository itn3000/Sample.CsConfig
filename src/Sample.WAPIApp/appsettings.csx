var ReturnValue = new MyConfiguration()
{
    a = "value1",
    b = 1,
    e = new MyConfiguration.SubConfiguration()
    {
        c = 1.1,
        d = "value2",
    },
    Logging = new MyConfiguration.LoggingSetting()
    {
        IncludeScopes = true,
        LogLevel = new MyConfiguration.LoggingSetting.LoggingLevel()
        {
            Default = "Verbose"
        }
    }
};
// var ReturnValue = new {
//     a = "b",
//     c = new {
//         d = "e"
//     },
//     Logging = new {
//         LogLevel = new {
//             Default = "Verbose"
//         }
//         ,
//         IncludeScopes = true
//     }
// };
