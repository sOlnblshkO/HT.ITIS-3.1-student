using Dotnet.Homeworks.DataAccess.Specs.Infrastructure;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Specs;

public static class UsersSpecs
{
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене gmail.com
    /// </summary>
    public static Specification<User> HasGoogleEmail => throw new NotImplementedException();
    
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене yandex.ru
    /// </summary>
    public static Specification<User> HasYandexEmail => throw new NotImplementedException();
    
    /// <summary>
    /// Возвращает true, если почта пользователя находится на домене mail.ru
    /// </summary>
    public static Specification<User> HasMailEmail => throw new NotImplementedException();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasGoogleEmail"/> или <see cref="HasYandexEmail"/> или <see cref="HasMailEmail"/>
    /// </summary>
    public static Specification<User> HasPopularEmailVendor => throw new NotImplementedException();

    /// <summary>
    /// Возвращает true, если длина имени пользователя больше 15 символов
    /// </summary>
    public static Specification<User> HasLongName => throw new NotImplementedException();

    /// <summary>
    /// Возвращает true, если параметр <code>Name</code> пользователя содержит пробел
    /// </summary>
    public static Specification<User> HasCompositeNameWithWhitespace => throw new NotImplementedException();
    
    /// <summary>
    /// Возвращает true, если параметр <code>Name</code> пользователя содержит дефис
    /// </summary>
    public static Specification<User> HasCompositeNameWithHyphen => throw new NotImplementedException();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasCompositeNameWithWhitespace"/> или <see cref="HasCompositeNameWithHyphen"/>
    /// </summary>
    public static Specification<User> HasCompositeName => throw new NotImplementedException();

    /// <summary>
    /// Возвращает true, если пользователь <see cref="HasLongName"/> и <see cref="HasCompositeName"/>
    /// </summary>
    public static Specification<User> HasComplexName => throw new NotImplementedException();
}