using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArriendoFeliz.Migrations
{
    /// <inheritdoc />
    public partial class FirstStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(Select Id from AspNetRoles where Id = 'e9eb409e-81bb-41a5-9305-555310363a59')
               BEGIN
                   INSERT AspNetRoles (Id, [Name], [NormalizedName])
                   VALUES ('e9eb409e-81bb-41a5-9305-555310363a59','Administrador','ADMINISTRADOR')
               END");

            // Insertar un usuario en AspNetUsers con el orden correcto de las columnas
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT Id FROM AspNetUsers WHERE Id = '4a2a3a7c-9bb0-446e-9071-2bbdeba6b847')
                BEGIN
                    INSERT AspNetUsers (
                        Id, 
                        UserName, 
                        NormalizedUserName, 
                        Email, 
                        NormalizedEmail, 
                        EmailConfirmed, 
                        PasswordHash, 
                        SecurityStamp, 
                        ConcurrencyStamp, 
                        PhoneNumberConfirmed, 
                        TwoFactorEnabled, 
                        LockoutEnd, 
                        LockoutEnabled, 
                        AccessFailedCount
                    )
                    VALUES (
                        '4a2a3a7c-9bb0-446e-9071-2bbdeba6b847', 
                        'admin@gmail.com', 
                        'ADMIN@GMAIL.COM', 
                        'admin@gmail.com', 
                        'ADMIN@GMAIL.COM', 
                        1, 
                        'AQAAAAIAAYagAAAAEIlmDoGcNEC3oE0UCbXp+bQkMArEh/4qpiRYUleRuDsZbZ8aDm2lhtiDUfjGQHvTUw==', 
                        'F53KQRTAGUUKZAA4DVFFRZI6KMLNRGPT', 
                        '5d0d15b7-2e23-4e54-b8eb-6cc38c10747a', 
                        1, 
                        0, 
                        NULL, 
                        1, 
                        0
                    )
                END");

            // Asignar el rol 'admin' al usuario
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT * FROM AspNetUserRoles WHERE UserId = '4a2a3a7c-9bb0-446e-9071-2bbdeba6b847' AND RoleId = 'e9eb409e-81bb-41a5-9305-555310363a59')
               BEGIN
                   INSERT AspNetUserRoles (UserId, RoleId)
                   VALUES ('4a2a3a7c-9bb0-446e-9071-2bbdeba6b847', 'e9eb409e-81bb-41a5-9305-555310363a59')
               END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = 'e9eb409e-81bb-41a5-9305-555310363a59'");
            migrationBuilder.Sql("DELETE AspNetUserRoles WHERE UserId = '4a2a3a7c-9bb0-446e-9071-2bbdeba6b847' AND RoleId = 'e9eb409e-81bb-41a5-9305-555310363a59'");
        }
    }
}
