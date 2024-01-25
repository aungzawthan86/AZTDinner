using ErrorOr;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail",
                                                             description: "Your email is already exists.");
    }
}