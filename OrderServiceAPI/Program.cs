using OrderServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opt =>
    opt.AddPolicy("All", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddGrpc();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseGrpcWeb(new GrpcWebOptions {  DefaultEnabled = true });
app.MapGrpcService<PostOrderService>().RequireCors("All");//.EnableGrpcWeb();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();