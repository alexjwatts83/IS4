// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IS4.is4aspid.Data;
using IS4.is4aspid.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IS4.is4aspid
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var alice = userMgr.FindByNameAsync("alice").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice"
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("alice created");
                }
                else
                {
                    //var result = userMgr.AddClaimsAsync(alice, new Claim[]{
                    //    new Claim("about_access", "true")
                    //}).Result;
                    //if (!result.Succeeded)
                    //{
                    //    throw new Exception(result.Errors.First().Description);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("alice about_access added");
                    //}

                    var claims = userMgr.GetClaimsAsync(alice);
                    foreach (var claim in claims.Result)
                    {
                        Console.WriteLine($"{claim.Type} = {claim.Value}");
                    }
                    Console.WriteLine("alice already exists");
                }

                var bob = userMgr.FindByNameAsync("bob").Result;
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "bob"
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("bob created");
                }
                else
                {
                    var result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim("cancel_access", "true")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    else
                    {
                        Console.WriteLine("bob cancel_access added");
                    }
                    var claims = userMgr.GetClaimsAsync(bob);
                    foreach (var claim in claims.Result)
                    {
                        Console.WriteLine($"{claim.Type} = {claim.Value}");
                    }
                    Console.WriteLine("bob already exists");
                }

                var alex = userMgr.FindByNameAsync("alex").Result;
                if (alex == null)
                {
                    alex = new ApplicationUser
                    {
                        UserName = "alex"
                    };
                    var result = userMgr.CreateAsync(alex, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alex, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alex Watts"),
                        new Claim(JwtClaimTypes.GivenName, "Alex"),
                        new Claim(JwtClaimTypes.FamilyName, "Watts"),
                        new Claim(JwtClaimTypes.Email, "alex.watts@minlog.com.au"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alex.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere else"),
                        new Claim("admin_access", "true")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("alex created");
                }
                else
                {
                    //var
                    Console.WriteLine("alex already exists");
                }

                AddUser("fleetgen", "General", "Fleet", "Pass123$", false, "general_access", userMgr);
                AddUser("fleetadmin", "Admin", "Fleet", "Pass123$", true, "general_access", userMgr);
                AddUser("locgen", "General", "Location", "Pass123$", false, "general_access_loc", userMgr);
                AddUser("locadmin", "Admin", "Location", "Pass123$", true, "general_access_loc", userMgr);
            }
        }

        private static void AddUser(string userName,string familyName,string firstName, string password,bool hasAdminAccess,string genAccess, UserManager<ApplicationUser> userMgr)
        {
            var alex = userMgr.FindByNameAsync(userName).Result;
            if (alex == null)
            {
                alex = new ApplicationUser
                {
                    UserName = userName
                };
                var result = userMgr.CreateAsync(alex, password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alex, new Claim[]{
                    new Claim(JwtClaimTypes.Name, $"{firstName} {familyName}"),
                    new Claim(JwtClaimTypes.GivenName, firstName),
                    new Claim(JwtClaimTypes.FamilyName, familyName),
                    new Claim(JwtClaimTypes.Email, $"{firstName.ToLower()}.{familyName.ToLower()}@minlog.com.au"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, $"http://{firstName}.com"),
                    new Claim(hasAdminAccess ? "admin_access" : genAccess, "true")
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Console.WriteLine($"{userName} created");
            }
            else
            {
                //var
                Console.WriteLine($"{userName} already exists");
            }
        }
    }
}
