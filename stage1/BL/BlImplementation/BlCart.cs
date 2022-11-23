
using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;
internal class BlCart:ICart
{
    IDal Dal = new DalList();
}