using ErrorOr;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCreditials => Error.Validation(code: "User.InvalidCred",
                                                             description: "Invalid Creditails.");
    }
}