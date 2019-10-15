using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace LightBilling.Mapping.Base
{
    public abstract class AbstractBaseMapper<TE,TD>
    {
        private readonly IMapper _mapper;

        public AbstractBaseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public virtual TD ToDto(TE entity)
        {
            return _mapper.Map<TD>(entity);
        }
        
        public virtual TE ToEntity(TD dto)
        {
            return _mapper.Map<TE>(dto);
        }

        public virtual List<TD> ToDto(IEnumerable<TE> entities)
        {
            return entities.Select(ToDto)
                .ToList();
        }
        
        public virtual List<TE> ToEntity(IEnumerable<TD> dtos)
        {
            return dtos.Select(ToEntity)
                .ToList();
        }
    }
}