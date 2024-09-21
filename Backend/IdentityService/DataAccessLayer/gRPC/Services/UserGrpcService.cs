using DataAccessLayer.Entities;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using UserGrpc;

namespace DataAccessLayer.gRPC.Services;

public class UserGrpcService(
    UserManager<User> userManager) 
    : UserGrpc.UserGrpcService.UserGrpcServiceBase
{
    public override async Task<CheckUserResponse> CheckUser(CheckUserRequest request, ServerCallContext context)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        
        return new CheckUserResponse{Exists = user != null};
    }
}