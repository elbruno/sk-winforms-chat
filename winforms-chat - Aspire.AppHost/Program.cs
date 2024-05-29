var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.winforms_chat>("winforms-chat");

builder.Build().Run();
