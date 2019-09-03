using ResgateIO.Service;
using System;

namespace Authorization
{
    class Program
    {
        static bool IsPublic = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Authorization microservice");

            ResService service = new ResService();
            service.AddHandler(">", new DynamicHandler()
                .SetAccess(req =>
                {
                    if (IsPublic || (req.Token != null && (string)req.Token["user"] == "admin"))
                    {
                        req.AccessGranted();
                    }
                    else
                    {
                        req.AccessDenied();
                    }
                }));

            service.SetOwnedResources(null, new[] { ">" });
            service.Serve("nats://127.0.0.1:4222");

            while (true)
            {
                Console.WriteLine(IsPublic ? "Access for everyone" : "Access only for admins");
                if (Console.ReadLine() == "QUIT")
                {
                    break;
                }
                IsPublic = !IsPublic;
                service.ResetAll();
            }
        }
    }
}
