using App.Base.Shared.Models;

namespace App.Base.Data
{
    public static class FakeDataBuilder
    {
        private static readonly string[] Names = new[]
        {
        "Tom", "Dick", "Harry", "Betty","Sally","John","Bert","Mary","Pam","Francesca","Sophie"
    };
        private static readonly string[] Streets = new[]
        {
        "Main Street", "Sidestreet","Marjoriebanks Street","31st Street","Strawberry Lane","Lost Street","Field Avenue"
    };

        private readonly static ICollection<SomeBaseParentModel> _data;
        static FakeDataBuilder()
        {
            _data =
                Enumerable.Range(1, 5).Select(index =>
                    new SomeBaseParentModel
                    {
                        Id = index,
                        Name = Names[Random.Shared.Next(Names.Length)]
                    })
            .ToArray();

            foreach (var item in _data)
            {

                item.Addresses =
                    Enumerable.Range(1, 2).Select(i2 =>
                new SomeBaseChildModel
                {
                    ParentFK = item.Id,
                    //Parent = item,
                    Id = i2,
                    Street = Streets[Random.Shared.Next(Streets.Length)],
                }).ToList();

            }

        }

        public static IEnumerable<SomeBaseParentModel> Get()
        {
            return _data;
        }
    }
}
