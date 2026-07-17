using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Extensions
{
    public static class DtoExtensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }

        public static Item AsEntity(this CreateItemDto createItemDto)
        {
            return new Item
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }

        public static Item AsEntity(this UpdateItemDto updateItemDto, Guid id)
        {
            return new Item
            {
                Id = id,
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
