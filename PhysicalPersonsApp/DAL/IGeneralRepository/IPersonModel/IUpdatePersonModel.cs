namespace DAL.IGeneralRepository.IPersonModel
{
    public interface IUpdatePersonModel
    {
        public Task UpdateName(string name, int Id);
        public Task UpdateSurname(string surname, int Id);
        public Task UpdateGender(string gender, int Id);
        public Task UpdatePrivateId(string privateId, int Id);
        public Task UpdateBirthDate(DateOnly birthDate, int Id);
        public Task UpdateCity(string city, int Id);
        public Task UpdateImage(string path, int Id);
    }
}
