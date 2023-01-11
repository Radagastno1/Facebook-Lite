using CORE;
namespace LOGIC;
public class QueryGenerator<T>
{
    public static string InsertQuery(T obj)
    {
        string query = string.Empty;
        switch (obj)
        {
            case User:
            query =  "INSERT INTO users(first_name, last_name, email, pass_word, birth_date, gender, about_me, role_id) " +
       "VALUES(@FirstName, @LastName, @Email, @PassWord, @BirthDate, @Gender, @AboutMe, 5); " +
       "SELECT LAST_INSERT_ID();";
                break;
            case Message:
                break;
            case Conversation:
                break;
            case Comment:
                break;
            case Post:
                break;
        }
        return query;
    }
    public static string SelectQuery(T obj)
    {
        string query = string.Empty;
        switch (obj)
        {
            case User:
            query = "SELECT u.id, u.first_name as 'FirstName', u.last_name as 'LastName', u.email," +
      "u.pass_word as 'PassWord', DATE_FORMAT(u.birth_date, '%Y-%m-%d') as 'BirthDate', u.gender, u.about_me as 'AboutMe', r.name as 'Role' " +
         "FROM users u " +
         "INNER JOIN roles r ON r.id = ur.roles_id WHERE u.is_deleted = false;";
                break;
            case Message:
                break;
            case Conversation:
                break;
            case Comment:
                break;
            case Post:
                break;
        }
        return query;
    }
    public static string UpdateQuery(T obj)
    {
        string query = string.Empty;
        switch (obj)
        {
            case User:
             query = "Update users SET first_name = @FirstName, last_name = @LastName, " +
         "email = @Email, pass_word = @PassWord, gender = @Gender, " +
         "about_me = @AboutMe WHERE id = @ID;";
                break;
            case Message:
                break;
            case Conversation:
                break;
            case Comment:
                break;
            case Post:
                break;
        }
        return query;
    }
    public static string DeleteQuery(T obj)
    {
        string query = string.Empty;
        switch (obj)
        {
            case User:
              query = "START TRANSACTION;" +
        "UPDATE users SET is_active = false WHERE id = @id;" +
         "UPDATE messages SET is_visible = FALSE WHERE sender_id = @id;" +
         "UPDATE posts SET is_visible = FALSE WHERE users_id = @id;" +
         "COMMIT;";
                break;
            case Message:
                break;
            case Conversation:
                break;
            case Comment:
                break;
            case Post:
                break;
        }
        return query;
    }
}