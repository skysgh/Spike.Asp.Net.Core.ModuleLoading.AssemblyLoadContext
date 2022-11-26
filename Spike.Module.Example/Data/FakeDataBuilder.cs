using App.Modules.Example.Models;

namespace App.Modules.Example.Data
{
    public static class FakeDataBuilder
    {
        private static readonly string[] Names = new[]
        {
        "Cats", "Dogs", "Fish", "Birds","Whales","Insects","Extra Terrestrials", "Plants","Microbes"
    };

        private readonly static ICollection<ExampleModuleModel> _data;
        static FakeDataBuilder()
        {
            _data =
                Enumerable.Range(1, 5).Select(index =>
                    new ExampleModuleModel
                    {
                        Id = index,
                        Name = Names[Random.Shared.Next(Names.Length)]
                    })
            .ToArray();

            var x = App.Base.Data.FakeDataBuilder.Get().ToArray();

            foreach (var item in _data)
            {

                item.SubChildren =
                Enumerable.Range(1, 2)
                    .Select(i2 =>
                                x[Random.Shared.Next(x.Length)])
                    .ToList();
            }

        }

        public static IEnumerable<ExampleModuleModel> Get()
        {

            return _data;
        }
    }
}
