namespace App.Interfaces.Common.Security;

public interface ITokenProvider
{
    string GenerateToken();
    bool VerifyToken();
    string ExtractSubject();
    object ExtractClaim();
}