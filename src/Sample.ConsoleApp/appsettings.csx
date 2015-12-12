#load "MyConfiguration.cs"
var ReturnValue = new MyConfiguration()
{
    a = "value1",
    b = 1,
    e = new MyConfiguration.SubConfiguration()
    {
        c = 1,
        d = "value2",
    }
};
// var ReturnValue = new 
// {
//     a = "b",
//     c = 1,
//     d = new
//     {
//         e = "f",
//         g = 1.0,
//     },
// };