namespace CursusAdministratie2021.Client.Core.CourseParsers {
    public interface ICoursePropertyParser<T> {
        T Parse(string text);
    }
}