using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView.CrudConsoleServices
{
    public class SupplierCrudConsoleService : ICrudConsoleService<Supplier>
    {
        private readonly SupplierManager _supplierManager;

        public SupplierCrudConsoleService(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public async Task Delete()
        {
            Console.WriteLine("Enter Id of supplier to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _supplierManager.Delete(id);
        }

        public async Task Add()
        {
            var manufacturerDto = await CreateModel();
            await _supplierManager.Add(manufacturerDto);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of supplier to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var supplierDto = await CreateModel();
            supplierDto.Id = id;

            await _supplierManager.Update(supplierDto);
        }

        public async Task<Supplier> CreateModel()
        {
            return await Task.Run(() =>
                                  {
                                      Console.WriteLine("Enter supplier name");
                                      var name = Console.ReadLine();
                                      Console.WriteLine("Enter supplier address");
                                      var address = Console.ReadLine();
                                      Console.WriteLine("Enter supplier phone");
                                      var phone = Console.ReadLine();

                                      var supplierDto = new Supplier
                                                        {
                                                            Name = name,
                                                            Address = address,
                                                            Phone = phone
                                                        };

                                      return supplierDto;
                                  });
        }
    }
}