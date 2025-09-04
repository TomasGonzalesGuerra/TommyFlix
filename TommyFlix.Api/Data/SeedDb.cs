using Microsoft.EntityFrameworkCore;
using TommyFlix.Api.Helpers;
using TommyFlix.Shared.Entities;
using TommyFlix.Shared.Enums;

namespace TommyFlix.Api.Data;

public class SeedDb(DataContext datacontext, IUserHelper userHelper)
{
    private readonly DataContext _datacontext = datacontext;
    private readonly IUserHelper _userHelper = userHelper;

    public async Task SeedAsync()
    {
        await _datacontext.Database.EnsureCreatedAsync();
        await CheckRolesAsync();
        await CheckSuperAdminAsync("El Tommy", "eltommy@yopmail.com", "322 311 420", "naruto.gif", UserType.SuperAdmin);
        await CheckUserAsync("Naruto", "naruto@yopmail.com", "322 311 460", "https://tse2.mm.bing.net/th/id/OIP.qmzNgDc5Qif3CKQhHPz0CwHaJe?w=1600&h=2048&rs=1&pid=ImgDetMain&o=7&rm=3", UserType.User);
        await CheckUserAsync("Brad Pitt", "brad@yopmail.com", "322 311 462", "https://www.famousbirthdays.com/headshots/brad-pitt-9.jpg", UserType.User);
        await CheckUserAsync("Angelina Jolie", "angelina@yopmail.com", "322 311 620", "https://th.bing.com/th/id/R.ea08e41477d34ca50ea1d471ae9a24c1?rik=syM1YQV3YAeA8A&riu=http%3a%2f%2fwww.pngall.com%2fwp-content%2fuploads%2f4%2fAngelina-Jolie-PNG-Download-Image-180x180.png&ehk=9PpQoKwZZnToSx13c8BKIY7KF8ZVFGienK24OKAGXTk%3d&risl=&pid=ImgRaw&r=0", UserType.User);
        await CheckUserAsync("Bob Marley", "bob@yopmail.com", "322 314 620", "https://th.bing.com/th/id/R.0775505a15a8846b6aa0930ab5e0d8dd?rik=wC1YaVdSPWOamQ&riu=http%3a%2f%2fwww.myfirstrecord.co.uk%2frecordpress%2fwp-content%2fuploads%2f2011%2f05%2fBob-Marley1-150x150.jpg&ehk=nOBcCpQOgc8iSniug5PCieOqW5fNq3ja%2faS%2bCWw9xxE%3d&risl=&pid=ImgRaw&r=0", UserType.User);
        await CheckUserAsync("Mila Azul", "mila@yopmail.com", "382 314 620", "https://wikisbios.com/wp-content/uploads/2022/11/1669490405_657_Mila-Azul-Height-Weight-Bio-Wiki-Age-Photo-Instagram.jpg", UserType.User);
        await CheckUserAsync("Sai Ambu", "miha@yopmail.com", "377 314 620", "https://tse4.mm.bing.net/th/id/OIP.ZltgcqHOJxCsj2Pf9IhKqQAAAA?rs=1&pid=ImgDetMain&o=7&rm=3", UserType.User);
        await CheckUserAsync("Hynata Hyuga", "hyna@tommy.com", "928 172 126", "https://pt.quizur.com/_image?href=https://img.quizur.com/f/img6149da08ee4b74.87549065.jpg?lastEdited=1632229911&w=600&h=600&f=webp", UserType.User);
        await CheckUserAsync("Ino Sarutobi", "ino@tommy.com", "928 172 129", "https://th.bing.com/th/id/R.cbca06d335b58ddea8eafd6f1207f994?rik=XIcp71ShwjKQ3g&riu=http%3a%2f%2ficons.iconseeker.com%2fpng%2f128%2fnaruto-vol-2%2fyamanaka-ino.png&ehk=m2lPEUWXtlWr9W%2fne%2bmOtCfLTzCrULFN5%2bNL%2b%2fPVciI%3d&risl=&pid=ImgRaw&r=0", UserType.User);
        await CheckRellenoAsync();
        CastMember? director = await _datacontext.CastMembers.FirstOrDefaultAsync(c => c.FullName == "Christopher Nolan");
        CastMember? actor = await _datacontext.CastMembers.FirstOrDefaultAsync(c => c.FullName == "Leo DiCaprio");
        CastMember? actor2 = await _datacontext.CastMembers.FirstOrDefaultAsync(c => c.FullName == "David Harbour");
        Gender? gender = await _datacontext.Genders.FirstOrDefaultAsync(g => g.Name == "Sci-Fi");
        Gender? gender2 = await _datacontext.Genders.FirstOrDefaultAsync(g => g.Name == "Romance");
        Gender? gender3 = await _datacontext.Genders.FirstOrDefaultAsync(g => g.Name == "Drama");
        Tag? tag = await _datacontext.Tags.FirstOrDefaultAsync(t => t.Name == "Classics");
        Tag? tag2 = await _datacontext.Tags.FirstOrDefaultAsync(t => t.Name == "Award-winning");
        Tag? tag3 = await _datacontext.Tags.FirstOrDefaultAsync(t => t.Name == "Trending");
        await CheckMoviesAsync(director!, actor!, gender!, gender2!, tag!, tag2!);
        await CheckSerieAsync(actor2!, gender!, gender3!, tag3!);
    }

    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.SuperAdmin.ToString());
        await _userHelper.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckSuperAdminAsync(string fullName, string email, string phone, string imageName, UserType userType)
    {
        var user = await _userHelper.GetUserAsync(email);

        if (user == null)
        {
            string imagePath = $"https://schoolbook2024.blob.core.windows.net/users/{imageName}";

            user = new User
            {
                FullName = fullName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Photo = imagePath,
                UserType = userType,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task<User> CheckUserAsync(string fullName, string email, string phone, string imagePath, UserType userType)
    {
        var user = await _userHelper.GetUserAsync(email);

        if (user == null)
        {
            user = new User
            {
                FullName = fullName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Photo = imagePath,
                UserType = userType,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task CheckRellenoAsync()
    {
        if (_datacontext.Tags.Any()) return;
        if (_datacontext.Genders.Any()) return;
        if (_datacontext.CastMembers.Any()) return;

        List<Tag> tags =
        [
            new() { Name = "Award-winning" },
            new() { Name = "Trending" },
            new() { Name = "Classics" },
            new() { Name = "Kids" },
            new() { Name = "New Release" },
        ];

        List<Gender> genders =
        [
            new() { Name = "Drama" },
            new() { Name = "Acción" },
            new() { Name = "Aventura" },
            new() { Name = "Wester" },
            new() { Name = "Comedia" },
            new() { Name = "Sci-Fi" },
            new() { Name = "Romance" },
        ];

        List<CastMember> cast =
        [
            new() { FullName = "Robert Downey Jr.", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQ4YvAPmO8BXcx9ProtT2ELROF3gKACybOauJvQFzCC7QHFNfBbO7cii03jXaBL7GQJR1FLgreNZ34Ta2AdSYr4v4SWLQ04t_xwPjVaSo1m" },
            new() { FullName = "Leo DiCaprio", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn3.gstatic.com/licensed-image?q=tbn:ANd9GcQcbqOH4dzt57bdZ0K36CHrRkhqgtGgwIrA7mHoA4M0cc8x239pMR-h28FlqERzrqW0GVMn1-Ok_LD0-KjYHnQCBki8Hqr50uPbUiPTn5UAhNDJa9Mtu-dp2JjcSYYNptQ97eLLy6_0DFEP" },
            new() { FullName = "Scarlett Johansson", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQvUV92L4N9fw-7hfn9wW24oQ02a2XWEBxpDuNqkx7yfyDvLt8qsDxCKn4e4JXndRG_wN5ZW97VvSv143gREwXWnUItYImRBm1sS_Utg1E5" },
            new() { FullName = "David Harbour", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSedPs8BfzWTUy4l4PMHuw0pjlIsATTWBCjamK_ZCt4mS2FvcxqpwOXCfK5rX6e5BcMuUVP7XnvqdL2t9J7abjJibN15XOcvRREslkBEQGLtg" },
            new() { FullName = "Christopher Nolan", Role = CastType.Director, PhotoUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSX4A-qr_vYV8mTSEFvFnW0FdGMVyRjzneotdDipdK8KhtG7M6qRrhf1bnwGgiTnr4zK_zTu4ZPJmO0G0DNJfFWKPTi3cJG3KIUj_QbltWY1w" },
            new() { FullName = "Vince Gilligan", Role = CastType.Director, PhotoUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSSUFt79HO1dzbDFzvMDqa4rCS8oGheR1Hh633YD4FMhcaaeVtmunROxH5YvsZm8bSSfSdEuALOKU7fie0M4w0OFIDhbMJbnrOqVHDxlRHcgg" },
        ];

        _datacontext.Tags.AddRange(tags);
        _datacontext.Genders.AddRange(genders);
        _datacontext.CastMembers.AddRange(cast);
        await _datacontext.SaveChangesAsync();
    }

    private async Task CheckMoviesAsync(CastMember director, CastMember actor, Gender gender, Gender gender2, Tag tag, Tag tag2)
    {
        if (_datacontext.Movies.Any()) return;

        Movie movie1 = new()
        {
            Title = "Inception",
            Description = "Un ladrón que roba secretos mediante el uso de tecnología para infiltrarse en los sueños.",
            ReleaseDate = new DateTime(2010, 7, 16),
            PosterUrl = "https://link/to/inception.jpg",
            Duration = TimeSpan.FromMinutes(148),
            Tags = [tag],
            Cast = [director],
            Genders = [gender],
            Ratings = [],
            WatchHistories = [],
        };

        Movie movie2 = new()
        {
            Title = "Titanic",
            Description = "La tragedia del trasatlántico vista a través del romance de Jack y Rose.",
            ReleaseDate = new DateTime(1997, 12, 19),
            PosterUrl = "https://link/to/titanic.jpg",
            Duration = TimeSpan.FromMinutes(195),
            Tags = [tag2],
            Cast = [actor],
            Genders = [gender2],
            Ratings = [],
            WatchHistories = [],
        };

        _datacontext.Movies.AddRange(movie1, movie2);
        await _datacontext.SaveChangesAsync();
    }

    private async Task CheckSerieAsync(CastMember actor2, Gender gender, Gender gender3, Tag tag3)
    {
        if (_datacontext.Series.Any()) return;

        Serie serie = new()
        {
            Title = "Stranger Things",
            Description = "Niños, ciencia y lo paranormal en Hawkins, Indiana.",
            ReleaseDate = new DateTime(2116, 7, 15),
            PosterUrl = "https://link/to/stranger_things.jpg",
            Tags = [tag3],
            Cast = [actor2],
            Genders = [gender, gender3],
            Seasons = [],
            Ratings = [],
            WatchHistories = [],
        };

        Serie serie2 = new()
        {
            Title = "Stranger Things 2",
            Description = "Niños, ciencia y lo paranormal en Hawkins, Indiana.",
            ReleaseDate = new DateTime(2016, 7, 15),
            PosterUrl = "https://link/to/stranger_things.jpg",
            Tags = [tag3],
            Cast = [actor2],
            Genders = [gender, gender3],
            Seasons = [],
            Ratings = [],
            WatchHistories = [],
        };

        Serie serie3 = new()
        {
            Title = "Fringe",
            Description = "La agente especial del FBI, Olivia Dunham es asignada a la División Suplementaria de la oficina, en la cual se investigan crímenes y sucesos inusuales. Asistiendo a Olivia en sus investigaciones se encuentra el científico, Dr. Walter Brishop, que alguna vez estuvo internado en una institución mental y su hijo alejado, Peter. Las pistas descubiertas durante las pesquisas, llevan al equipo a creer que los crímenes son realmente parte de un patrón más amplio y que una corporación importante está involucrada.",
            ReleaseDate = new DateTime(2013, 1, 18),
            PosterUrl = "data:image/webp;base64,UklGRooQAABXRUJQVlA4IH4QAAAwQwCdASq8AGsAPtFAtFooIqgoE6EAGglla5VaLDfhh77/HP9j4S+Yr6FJtuP+xGeT+88B+Ai9LtEb9f7nzd+zPRp4Nf4f/p+wN/OP8X+xfvD/7Hk5/aP+F7DPTG8m5J23R0pvG1edFXTEH6YY72Aa5Y9GuGuLDYlLtC5giGyOgPk+pqW26xO40/+S3TrjPIUnhXbyqAwcp0R+Fw/2/XH/MaQv9C3r60akSI86oLYf7qAVDr9Hd6rv6c/jw6pmS8Pcx4Cq9I9mXNUoPcVBQQCXjrzAJcPuHgNpe/xIsxnQFEDfoGufFUKfmGvb91mvMLoMfysrrEvj+x0RBoZ8B32c/8Eb5boT3iid5i6sENmHJp2sFKLuN8rXEeN5fzFHEdik70iHT/wfE08X8pj8o6buFJZCwpib2mGz0XCfS0/09oq4hCKYsLxSMIZiSpN3pruERwZSaydKsUxL96E4HnQS8/X2kFph/wb8BCFIyBmrZn0pu2x53OXzNXOB31utIRzNvXpGWhEXHG+AG05pGAySrKHiNC/jyKdEDwR+js/2FB4Tvg9MR0f8RumL/G8VOH2xJTq/TZLIaOqStx9UAhZ+w+QaKdJTPUGj4NoHksLsUMski+RcHhOGILjyjHyLq1JzpeiW8LhqjhxPE6RlOAuqgFYaWHK3YhRzGkefAeF66rCO7y+L55AxpY+G+wtG++Rg6yVk14aquPOGTSQxTwYQNAAA/vxErPiyB5xwREGm/6zNSSzrBrBqSvv9uk3oBemoyJu9wYFKtFeAmuBT5+P55hws7FlR5rIiQXkix8CcuSBe6HtscRkKwVCuOVosoHNov0+zpq4Jbz+VP6mILF0cOpA9y/mZPyGO5YmZcP8OkMm3s0xxNxj2QgzXFxyN77q0OopccKobpe6+wYYjvkXNvHCLdh3xFUVarQT/5XVTAwR5/cMG/L3JGhEZEB5MWTgPMxPZG4gu/A9jdK92n5fa//qOdPMFxqQJ6r96X/n/n9iGtrz9bS/AqRNNtEliaNZ5WDXMTJ+BYLlGWfZ8o6jkuv++jJoMOOfqBWXFDLlkTJ3pq2pE9yj2RajtTmPzexQP44OHMJHWXGO+QOKej2EJ1gf60BHE0CJbhbssyrbQh5C786QxwcDAsG0X+jW2fSenddvJtfXYDd6AoR8iaUHl/cje0+FRsawmZJreb4/ClYPZclVWdVpP/Of9egUcvzbirdeOVjIqUaS6MQt4uWYfvXLqWGqfKhLPnd5dRV8Q4jc0Vi9UOdHy3GJQNYjs6mR8ZqDOGthdHX+6l+/5V02rddaX3c6ejwbGiItcLHQeN5Gnb+3WkKSs2aNjBbqmkppOBV2k4+fYN5SJsUdzugyTbHZGuPVZbNTcpSOS0tvKLNXn1Ofsj0BlGCCTvcjXeerXrTxrzjeo7xXIK/VdRO3HUxcHWradIn4xNOBPZgzRrYg30q782PFj15sRdrPIR2lmsQWHfhErektzoNK+eOV3A3P9/lYoyHO1ITyh3Br/u6PYC+L2uLJgUL5TpuGxRcNoB0p2pHvyH9TVWyvlHhs8q3kdOv0e+6DOsXr/IEh8sHdVcWW9aiqOsjDcA/1qSpQSTcW0Uw/s/OOEbXfe/aJuM+z+ZJhBy1oTuO5rHRnx/5RgxCliphbqOdh0XeC0ZBghRpKjFw5dMISdc5lVAu6h/Tq7u0HeJFYJwpyJtOFf0AgsABVLvKbInl5jhZIutuLhZ3gkynAU1B3S+iddomRfEyaPCoE+f3zyNbddbow7IEXpfIxxnwdcyOfCyNW5FOxEdf6pnPERqx4j1LCj4/Mnve2hYqMHWXfX8QI7QdcQXR+Mmp9125zoTaQG2hFUGL3xd2R8K+FhQ5VtcBv+bXsXMs7xvybxwUBxSw4iUGQQ2Kyo2EYNwhnYgEIx9+3UYM4gX0J4Bc+we2QD077QocpL4g2Z1VdvLKrJPRwIV9W8kbb2xZ1dsw273tA727B8MnTMWZEsZqC6ub9FedHDXJHyMljUaHp0TkNw1ozZefmiMLecKPgWN859+GIaPLz2JjKbHvMVi9IAJ5UP5hsqcINY3xoFxP9fyejWV6Inyqs7wIO6tp9MK41/KDLm4QnOMHhWQZjQ5eGgoL24vAowbTCaZ2gQy90L2S4kzDq2CLtAA1O/8b7gYLhx09YvcKh0GowArBCFJ21pX2qPSTOemdiBRUCx96TmWeZZMauypICIp4tFacD8t0aylHP0y2fQYkDV5p9VJTj4Z4cco4QbrgmpKktP3+c0QcfSvPgHQibc18EOily4Foo2492EUFeK1WWU5SacmPl2mPB0IeyGvH7b+7rhiCUw3evq/5WEqblXTaPgUI+jXDqKCKKNJ2yh1aUvMolOwn/Xk5l7vxzGbg9NunQ6VttMdmKZzxg2DUmbIyYZwh0Z2QWIJJAI+JTAJeq4rC84ol1sB/GtpWH6VqMB//sDV2AsL471EDU16oV9GMiVeLsAB4h4km+1eELjDpar9QF4LRSElLj0u7PiqdAIILIDspqY0gzmBt7AuinxLl2jQEn6x3MW3e2OwIhJijl2WvhqQ8XwYz1Icxx919ob2cu2ryOrhnCyFbq+Q/U550nLWDce831mlWia6w0s1/jeJaNqhcetSLZL1lWEujoKFFgct2g4+MD6H3uV6I85I62WAuJK0F6IdUWIPZ2jFfmSgr03Klv6AMASGawgKZ8rRbYECZZVQ8XvzGEc7SW9RnSHcQpVJfB9bSihYr+7P8Kn5/896op3B0gw4YL+MCpF/NYWq5ML5iOXHRcEzBO9BENqT/f14ZyGi7trQvxAruo6rpm8Otq3pdMI0Bsj1GtFqGaE303QGqtYeNj4armTLSDPXZyFQPSY+fLoefvHitWUM8EXq4q1yPHPTiY7MBGBeMY64eULdysNmwuy3Nq/JbESwvAATP55Tkhb2NGKabXw9K3bRjBMuE/yxG0bWxnTcTF4LWnu71qyEQMQmB8KbQLyUZvX0FAmmb82Z4qOime7E03Q2sTXcBSTZ9u/LHT0sCTVAI2HXR543wMvAHct6hKdFobGgul8Rd3Xd6eX9x59VLkhBnvdraqxMrYT1MeUW1R2qcls3nv2SughmxZq8R6Wt7hEGoyR665oO8bzDrh9cyOb7t36b3uzYV+wcGkD9fqC4St3xYrEf5Kb3jIG/TXdGb8AUr2kdQfEbALJpO8pWznXcSpYp/bAbVE6S7gRob1JQLTi8ZaQGt/OGrnRayeWlbwpQ8cQ9/5XbkOC6/rapl5CVOaA9axcVJt3wCWRH4rZ0YvfZbAeN7BWmc2xmlh2TlTZ0D9D6NmbNYRNenSgowPfc9obSkR13eOytBD2jTVwgqcWFp8RA/hjRYG8183aY2tCGTBZsJUDjsIa0su23C1NLby/UETyJ6j5Wz0freDp3opULmk5C7dLKjVXTxghJ9/ge782PkEqavhLNPixdXgKmvIV7I0v4kURvIegfGK4PIuH3JoqQUZCnyLJ9UDA9dxHd83i1t7MwwradH/yzERx5CEgaexsCTcP6XKnR49Hu+Siz/Tctb/HuogaHxCbeDDDN1wh3iIRqSvVztyonuWhD0hl4Sd1zmusmDojOLiOVuZwdDL5rlwAkHiM2NcNEMmhi08aUvSl7FZfrK0LsA/4HS85MsNHAXAj34g2/r9+tfxq2WYbQnYMGsMQZnjzXosv2kqI7vtW02i0Jz4B/IpPmTEa02QWBGn4GxoGe542cAWzzrDLOWAM/fiM9r5Lse2tQusU2DbEMbanDRs9l1uzlvNsoQmYgKIvepJ94eplmMQixZWMZg3CV9mhML4prlHNJIkqUVYJ6c3bLeXYlVyVSd9hjYChuYMplWv1hmzhGgTtLNULX1zL6y5NKZKHQDy9fCcziBhUN8jpvTTFEqFEn58/M4sw8kmKEBkCvT8IJTH9PjPJBmzSDQ0ZEVx2QfY8FNdR1JKzpoUWiUDXMQBohtNWBxCfF+hYsNcHffpsLmc/oga84ZOPzsEXlKKn0+spLFgvLEKZpsfPbozBq9/v6pZkvLaa+9BZeXAiL/IgPq7oEPZiwoK7iC4KiA7PFZZxie0f5rznJ38kKCJ25D10YAzHF6uPTcMdOx5El649LhoGRIsW89nul7//8OMbqWwEUXPeg1SJ0ktu0F+acWdfS45o7vZGs+xkh8xw2lG/OTGlWZ8w0YKP5b4i+JM5CFFzClchKuP34e9t6p9TXyjKAg7Z24H7gxSZcjd/WmuNExdpFFOY9GESsRcWbjoiWK2EPYmtojA98nU98h+PhHIrWDh0IoeYMXr+aeoLs9rTsDtvSUEQ9u38smAgJE+DOcCeWaulTmwo1KsZv6PjuK/EzUJFfj6Z5HGN4BiGry2ooSUUIy70sWTpZf4Ifo3lr/pON1xYxXrw3+9snjj0px3hmJPmIAWjS3f+cAIsiq3WDW/7Nrh7Wy+iRTVaAN3/CHMi33TC+cf1PshUWgbueYtudLkdx6czZsy4u/HlsqlJl10VoaScAObGlrtfnL6k05uVmn9BVonvV+sOkdT8/B1iw/8y+1sDoI2oak4u9sgamVt3Zfffh8JJ7CKFBy+V2czU8QFkB1RezFSEtvjg24r6dm/tRb9AsjLCJbRKHubnk/pbQPn5zXLLaf0+o/CPH+uHLuRO+drcxgVXrbE7C3AhZSpNMfDTvKrlitONrfrd1K1I1/jFCtBa3NYt+ir0V5q6i1WENRuzHqAxAUvvVJIo/7SmbnB1JM2T5+3njpDJBw81dFGxvikGofKqeclT9sSWPwyeFbC+tW8il3buE8CI61acW2nZ3XV3nksmTa670hjYDkaNn7KKvTAdrz2MmTYPwmUX+jlgeQdSTJDGBRAlmjxg/MyihTHv3Z7NPSaTj/b3WxwZsEPaUPb81jOEAMPoW08akHMl3zH0aMGU+xx/LbP/f82bl4F1NEdONXbAwVXl6IVf4c7Falxq/U+oo23XDGvwiYujgK8AQZrm2RoCwOdDqTdGATComWnijTnCtRxdQYnPdcy3kFvgMo0HI4/a2U7TVOjbtiA9FzzdIXygb+8CuqOngyzduMkCXfqxVLwJpEqNlB8GWaV6+EQX82/PfPdSlJ8JaHyktilzVKMNlQCA1BCDsrhLyTaG6Qasxg6G3ZpzuLwhdBrs/RDXlMxZaipOtpyB2bJWsgttizyuTrSE1PTJCqpwufNL4Ertg/08Fwai552ojU0Q+d8eWrVb+LcbnGnn9RGx8n5RsBmf/PREQIDyqAIhRTxRUq9tWZ1XgKh3OBJu6NCpCrbRc7KwHC+yz2g1AXXjOBHPv9DnRqRuDggUoybyOBICnWLfaGOPc+7Zdu/4sTdhg31hJ7dM2RwZbpUiLyKYSlpgYbFaAK1iumVQfVnqjtfqctY7nIlMUUt7IlzLojdkGQKWHwCNkytDXWo0/UPVxdlSSp7293WXQwnC2os5gkBzIgu0NDbhkEgEHbwXDCXhRR6kpVYtiXp6nB41XIQYHDRqNEGjJWtIB/qoDHjRWa09e7reKu/2VoU3fNpnRR+29Y8NIVKdCCmc8jTTREj5oYAI0kHuP7wBDIpPE9Nl5ZEveMyhy2TL7LSimhib8q7jAYO7tV1um3yb4QfZ4AAA",
            Tags = [tag3],
            Genders = [gender, gender3],
            Cast = [],
            Seasons = [],
            Ratings = [],
            WatchHistories = [],
        };

        serie.Seasons!.Add( new()
        {
            SeasonNumber = 1,
            Episodes =
            [
                new() { Title = "The Vanishing of Will Byers", EpisodeNumber = 1, Duration = TimeSpan.FromMinutes(47) },
                new() { Title = "The Weirdo on Maple Street", EpisodeNumber = 2, Duration = TimeSpan.FromMinutes(55) }
            ]
        });

        serie.Seasons.Add( new()
        {
            SeasonNumber = 2,
            Episodes =
            [
                new() { Title = "Madmax", EpisodeNumber = 1, Duration = TimeSpan.FromMinutes(48) },
                new() { Title = "Trick or Treat, Freak", EpisodeNumber = 2, Duration = TimeSpan.FromMinutes(56) }
            ]
        });

        serie2.Seasons.Add( new()
        {
            SeasonNumber = 1,
            Episodes =
            [
                new() { Title = "Madmax", EpisodeNumber = 1, Duration = TimeSpan.FromMinutes(48) },
                new() { Title = "Trick or Treat, Freak", EpisodeNumber = 2, Duration = TimeSpan.FromMinutes(56) }
            ]
        });

        serie3.Cast.Add(new() { FullName = "Anna Torv", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRIRVI56EdqFed77d08NO2yt52lGKaKELtrFNxZizBj2uCFghICoCRMlFIZKNqgKxcYmmKrbFtIUMdwidzKGEBvlrehlFP-p96_mXVIMsQk" });
        serie3.Cast.Add(new() { FullName = "John Noble", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSBS9ptuXCV46uGIrfiy6A4tnloY671BBsrnqQzkUfgjQKTvSlWEY-lBRoU6H3ATJNXZJY_mtteojO36i2yLmwmYj10QgiXCBTIhM8fbEBEw" });
        serie3.Cast.Add(new() { FullName = "Joshua Jackson", Role = CastType.Actor, PhotoUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSHmjYvTeD_8fajSNmY1ZjlRHCqcQaRAs-wvBRp5XTgrtOwBeh-0s72xg-FZ0NWDKkoy9Xo0iNeAIIGeKPGXUtdPL5f05Qs6RDDZWH3D5Q9PA" });
        serie3.Cast.Add(new() { FullName = "J.J. Abrams", Role = CastType.Director, PhotoUrl = "https://encrypted-tbn1.gstatic.com/licensed-image?q=tbn:ANd9GcRgbCTnLm-qjDLO-qxTdOBSa-i2Fe7SzM8f5Kg155pstsBxn7K3H6Fm7tKHRBiE3iskJy73Ug-BBHHlMq7j56bjj5szOwVkqGgl842Du_UD5OJfBSdDioIdegaL7x1NL4BYMkKW0qFyxos" });
        serie3.Seasons.Add( new()
        {
            SeasonNumber = 1,
            Episodes =
            [
                new() { Title = "Pilot", EpisodeNumber = 1, Duration = TimeSpan.FromMinutes(45) },
                new() { Title = "The Same Old Story", EpisodeNumber = 2, Duration = TimeSpan.FromMinutes(50) },
            ]
        });
        serie3.Seasons.Add( new()
        {
            SeasonNumber = 2,
            Episodes =
            [
                new() { Title = "A New Day in the Old Town", EpisodeNumber = 1, Duration = TimeSpan.FromMinutes(42) },
                new() { Title = "Fracture", EpisodeNumber = 2, Duration = TimeSpan.FromMinutes(48) },
            ]
        });


        _datacontext.Series.Add(serie);
        _datacontext.Series.Add(serie2);
        _datacontext.Series.Add(serie3);
        await _datacontext.SaveChangesAsync();
    }


}
