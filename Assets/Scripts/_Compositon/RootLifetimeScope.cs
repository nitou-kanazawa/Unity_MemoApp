using VContainer;
using VContainer.Unity;
using Project.Domain.Memos.Repository;
using Project.Infrastructure.SQLiteNet.Memos;

namespace Project.Composition {
    
    public sealed class RootLifetimeScope : LifetimeScope {
        
        protected override void Configure(IContainerBuilder builder) {

            // Repository
            builder.Register<SQLiteMemoRepository>(Lifetime.Singleton).As<IMemoRepository>();


        }
    
    }
}
