using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace LightBilling.Mapping
{
    public abstract class AbstractBaseMapper<TE,TD>
    {
        private readonly IMapper _mapper;

        public AbstractBaseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public TD ToDto(TE dto)
        {
            return _mapper.Map<TD>(dto);
        }
        
        public TE ToEntity(TD entity)
        {
            return _mapper.Map<TE>(entity);
        }

        public List<TD> ToDto(IEnumerable<TE> entities)
        {
            return entities.Select(ToDto)
                .ToList();
        }
        
        public List<TE> ToEntity(IEnumerable<TD> dtos)
        {
            return dtos.Select(ToEntity)
                .ToList();
        }
    }
}