using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;

namespace DATABASE;

public class DeletionDB : IData<User>
{
    public int? Create(User obj)
    {
        //lägg till på deleted_user table
        throw new NotImplementedException();
    }

    public int? Delete(User obj)
    {
        //om man nu behöver deleta därifrån?
        throw new NotImplementedException();
    }

    public List<User> Get()
    { //hämta users från deletade table
        throw new NotImplementedException();
    }

    public int? Update(User obj)
    {
        //uppdatera något? vet ej
        throw new NotImplementedException();
    }
}
