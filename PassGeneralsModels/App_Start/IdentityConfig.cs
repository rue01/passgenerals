using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PassGeneralsModels.Models;

namespace PassGeneralsModels
{

    //If we wanted to extend the Identity implementation in our Web Api application to include Roles, and some database initialization, this file is where we would most likely define and configure a RoleManager and DbInitializer.

    //// IF YOU DON'T USE EF MIGRATIONS, USE THE CLASS:  public class ApplicationDbInitializer BELOW
    //// ApplicationRoleManager HAS BEEN REPLACED BY ApplicationSignInManager
    //
    //public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    //{

    //    protected override void Seed(ApplicationDbContext db)
    //    {
    //        InitializeIdentityForEF(context);
    //        base.Seed(context);
    //    }


    //    public static void InitializeIdentityForEF(ApplicationDbContext db)
    //    {
    //        var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
    //        var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
    //        //var hasher = new PasswordHasher();
    //        //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUserManager>(db));
    //        //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

    //        const string name = "admin@admin.com";
    //        const string password = "passW0rd@123";
    //        const string roleName = "Admin";

    //        //Create Role Admin if it does not exist
    //        var role = roleManager.FindByName(roleName);
    //        if (role == null)
    //        {
    //            role = new IdentityRole(roleName);
    //            var roleresult = roleManager.Create(role);
    //        }

    //        var user = userManager.FindByName(name);
    //        if (user == null)
    //        {
    //            user = new ApplicationUser { UserName = name, Email = name };
    //            var result = userManager.Create(user, password);
    //            result = userManager.SetLockoutEnabled(user.Id, false);
    //        }

    //        // Add user admin to Role Admin if not already added
    //        var rolesForUser = userManager.GetRoles(user.Id);
    //        if (!rolesForUser.Contains(role.Name))
    //        {
    //            var result = userManager.AddToRole(user.Id, role.Name);
    //        }
    //        //context.SaveChanges();


    //    }
    //}


    ///********* public static ApplicationUserManager Create is the same for all 3//////////////
    ///********* how do you get phone authorization

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext db) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    ////// Configure the application role manager which is used in this application.
    ////public class ApplicationRoleManager : RoleManager<IdentityRole>
    ////{
    ////    public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
    ////    : base(roleStore)
    ////    { }

    ////    public static ApplicationRoleManager Create(
    ////        IdentityFactoryOptions<ApplicationRoleManager> options,
    ////        IOwinContext context)
    ////    {
    ////        var manager = new ApplicationRoleManager(
    ////            new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
    ////        return manager;
    ////    }
    ////}

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
