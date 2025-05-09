# Exercises-and-MacroNutrients-Tracker-API-asp.net

Track and manage your personal exercises, including rep ranges, number of sets, and weights used. When you first submit an exercise, it’s marked as an "exercise base" to allow progress tracking for reps, sets, or weight. You can also log your daily caloric and macronutrient intake, with an optional body weight measurement for that day.



## Getting Started

### Set the connection string with secret manager

```powershell
    dotnet user-secrets set "ConnectionString:ExerciseDbConnectionString" "Server=[ServernName] or localhost;Database=[DatabaseName];User Id=user;Password=[PASSWORD GOES HERE];TrustServerCertificate=True;"
```

### Set the JWT singing key with secret manager

```powershell
    dotnet user-secrets set "JWT:SigningKey" "SIGNING-KEY-GOES-HERE"
```
recommend using https://jwtsecret.com/ to generate a key

### Starting up Database
```powershell
    dotnet ef database update
```

## AUTHOR
me: (https://github.com/CannaAz/)

## LICENSE
[MIT License] (https://github.com/CannaAz/Exercises-and-MacroNutrients-Tracker-API-asp.net/blob/dev/LICENSE)