namespace DocumentFormat.OpenXml.Flatten.Framework
{
    class OpenXmlCompositeElementFlattenProvider<TElement>
        : ElementFlattenProvider<TElement>
        where TElement : OpenXmlCompositeElement
    {
        public OpenXmlCompositeElementFlattenProvider(params TElement?[] elementList) : base(elementList)
        {
        }

        public T GetOrCreateElement<T>() where T : OpenXmlElement, new()
        {
            var result = Main.GetFirstChild<T>();

            if (result == null)
            {
                foreach (var element in ElementList)
                {
                    if (element is null) continue;
                    result = element.GetFirstChild<T>();
                    if (result != null)
                    {
                        result = (T) result.Clone();
                        Main.AddChild(result);
                        return result;
                    }
                }
            }

            if (result is null)
            {
                return Main.GetOrCreateElement<T>();
            }

            return result;
        }
    }
}
