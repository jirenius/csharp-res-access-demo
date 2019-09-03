using ResgateIO.Service;
using System;

namespace Authentication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Authentication microservice");

            ResService service = new ResService("authentication");
            service.AddHandler("user", new DynamicHandler()
                .SetAuthMethod("login", req =>
                {
                    if ((string)req.Params["password"] == "mysecret")
                    {
                        req.TokenEvent(new { user = "admin", foo = "bar" });
                        req.Ok();
                    }
                    else
                    {
                        req.InvalidParams("Wrong password");
                    }
                })
                .SetAuthMethod("logout", req =>
                {
                    req.TokenEvent(null);
                    req.Ok();
                }));

            service.Serve("nats://127.0.0.1:4222");
            Console.ReadLine();
        }
    }
}
