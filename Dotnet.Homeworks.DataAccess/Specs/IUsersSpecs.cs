using Dotnet.Homeworks.DataAccess.Specs.Infrastructure;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Specs;

public interface IUsersSpecs
{
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене gmail.com
    /// </summary>
    public Specification<User> HasGoogleEmail();
    
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене yandex.ru
    /// </summary>
    public Specification<User> HasYandexEmail();
    
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене mail.ru
    /// </summary>
    public Specification<User> HasMailEmail();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasGoogleEmail"/> или <see cref="HasYandexEmail"/> или <see cref="HasMailEmail"/>
    /// </summary>
    public Specification<User> HasPopularEmailVendor();

    /// <summary>
    /// Возвращает true, если длина параметра <code>Name</code> пользователя больше 15 символов
    /// </summary>
    public Specification<User> HasLongName();

    /// <summary>
    /// Возвращает true, если параметр <code>Name</code> пользователя содержит пробел
    /// </summary>
    public Specification<User> HasCompositeNameWithWhitespace();
    
    /// <summary>
    /// Возвращает true, если параметр <code>Name</code> пользователя содержит дефис
    /// </summary>
    public Specification<User> HasCompositeNameWithHyphen();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasCompositeNameWithWhitespace"/> или <see cref="HasCompositeNameWithHyphen"/>
    /// </summary>
    public Specification<User> HasCompositeName();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasLongName"/> и <see cref="HasCompositeName"/>
    /// </summary>
    public Specification<User> HasComplexName();
}