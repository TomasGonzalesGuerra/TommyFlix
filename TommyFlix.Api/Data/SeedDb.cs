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
            new() { FullName = "Robert Downey Jr.", Role = CastType.Actor },
            new() { FullName = "Leo DiCaprio", Role = CastType.Actor },
            new() { FullName = "Scarlett Johansson", Role = CastType.Actor },
            new() { FullName = "David Harbour", Role = CastType.Actor },
            new() { FullName = "Christopher Nolan", Role = CastType.Director },
            new() { FullName = "Vince Gilligan", Role = CastType.Director },
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
            ReleaseDate = new DateTime(2016, 7, 15),
            PosterUrl = "https://link/to/stranger_things.jpg",
            Tags = [tag3],
            Cast = [actor2],
            Genders = [gender, gender3],
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

        _datacontext.Series.Add(serie);
        await _datacontext.SaveChangesAsync();
    }


}
