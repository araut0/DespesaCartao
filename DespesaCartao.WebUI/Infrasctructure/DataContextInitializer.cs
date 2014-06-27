using System.Data.Entity;
using DespesaCartao.Domain.Concrete;

namespace DespesaCartao.Infrasctructure
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<EFDespesaCartaoContext>
    {
    }
}