using System.Reflection;

namespace Profile.Extentions
{
    public static class MppningExtentions
    {

        public static TDestination MapTo<TDestination>(this object source)
        {

            ArgumentNullException.ThrowIfNull(source, nameof(source));
            TDestination destination = (TDestination)Activator.CreateInstance(typeof(TDestination))!;


            var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var destinationProperty in destinationProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name && x.PropertyType == destinationProperty.PropertyType);
                if (sourceProperty != null && destinationProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
            return destination;
        }
    }
}
