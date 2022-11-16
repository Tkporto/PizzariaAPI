using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Instancia da API na memoria 
//builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("List"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Instancia da API em Sqlite com a conexao
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzaria.db";
builder.Services.AddSqlite<DataContext>(connectionString);
var app = builder.Build();

//INICIO PIZZA CRUD
app.MapGet("/", () => "Hello World!");


app.MapGet("/pizzas", async (DataContext db) =>
    await db.Pizzas.ToListAsync());


app.MapGet("/pizzas/{id}", async (int id, DataContext db) =>
    await db.Pizzas.FindAsync(id)
        is Pizza pizza
            ? Results.Ok(pizza)
            : Results.NotFound());


app.MapGet("/pizzas/buscar/{sabor}", (string sabor, DataContext db) =>
{
    return db.Pizzas.Where(t => t.Sabor.Equals(sabor));
});


app.MapPost("/pizzas/cadastrar", async (Pizza pizza, DataContext db) =>
{
    db.Pizzas.Add(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});


app.MapPut("/pizzas/alterar/{id}", async (int id, Pizza inputPizza, DataContext db) =>
{
    var pizza = await db.Pizzas.FindAsync(id);

    if (pizza is null) return Results.NotFound();

    pizza.Sabor = inputPizza.Sabor;
    pizza.Valor = inputPizza.Valor;

    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/pizza/deletar/{id}", async (int id, DataContext db) =>
{
    if (await db.Pizzas.FindAsync(id) is Pizza pizza)
    {
        db.Pizzas.Remove(pizza);
        await db.SaveChangesAsync();
        return Results.Ok(pizza);
    }

    return Results.NotFound();
});

//FIM PIZZA CRUD
//INICIO USUARIO

app.MapGet("/usuarios", async (DataContext db) =>
    await db.Usuarios.ToListAsync());


app.MapGet("/usuarios/{id}", async (int id, DataContext db) =>
    await db.Usuarios.FindAsync(id)
        is Usuario usuario
            ? Results.Ok(usuario)
            : Results.NotFound());


app.MapGet("/usuarios/buscar/{cpf}", (string cpf, DataContext db) =>
{
    return db.Usuarios.Where(t => t.Cpf.Equals(cpf));
});


app.MapPost("/usuarios/cadastrar", async (Usuario usuario, DataContext db) =>
{

    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
});


app.MapPut("/usuarios/alterar/{id}", async (int id, Usuario inputUsuario, DataContext db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);

    if (usuario is null) return Results.NotFound();

    usuario.Nome = inputUsuario.Nome;
    usuario.Cpf = inputUsuario.Cpf;

    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/usuarios/deletar/{id}", async (int id, DataContext db) =>
{
    if (await db.Usuarios.FindAsync(id) is Usuario usuario)
    {
        db.Usuarios.Remove(usuario);
        await db.SaveChangesAsync();
        return Results.Ok(usuario);
    }

    return Results.NotFound();
});


//FIM USUARIO
//INICIO MESAS
app.MapGet("/mesas", async (DataContext db) =>
    await db.Mesas.ToListAsync());


app.MapGet("/mesas/{id}", async (int id, DataContext db) =>
    await db.Mesas.FindAsync(id)
        is Mesa mesa
            ? Results.Ok(mesa)
            : Results.NotFound());


app.MapPost("/mesas/cadastrar", async (Mesa mesa, DataContext db) =>
{
    db.Mesas.Add(mesa);
    await db.SaveChangesAsync();
    return Results.Created($"/mesas/{mesa.Id}", mesa);
});


// FIM MESAS
//INICIO PEDIDOS
app.MapGet("/pedidos", async (DataContext db) =>
    await db.Pedidos.ToListAsync());


// FIM PEDIDOS


app.Run();


