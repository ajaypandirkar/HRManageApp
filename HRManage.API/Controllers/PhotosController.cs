using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HRManage.API.Data;
using HRManage.API.DTOs;
using HRManage.API.Helpers;
using HRManage.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HRManage.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        public IDatingRepository _repo { get; }
        public IMapper _mapper { get; }
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IDatingRepository repo, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;          
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret     
            );

            _cloudinary = new Cloudinary(acc); 
        }


        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDTO>(photoFromRepo);

            return Ok(photo);
        }

        // [HttpPost]
        // public async Task<IActionResult> AddPhotoForUser(int userId,
        //     PhotoForCreationDTO photoForCreationDTO )
        // {
        //     if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //         return Unauthorized();

        //     var userFromRepo = await _repo.GetUser(userId);

        //     var file = photoForCreationDTO.File;

        //     var uploadResult = new ImageUploadResult();

        //     if(file.Length > 0)
        //     {
        //         using (var stream = file.OpenReadStream())
        //         {
        //             var uploadParams = new ImageUploadParams()
        //             {
        //                 File = new FileDescription(file.Name, stream),
        //                 Transformation = new Transformation()
        //                     .Width(500).Height(500).Crop("fill").Gravity("face")
        //             };

        //             uploadResult = _cloudinary.Upload(uploadParams);
        //         }
        //     }

        //     photoForCreationDTO.Url = uploadResult.Uri.ToString();
        //     photoForCreationDTO.PublicId = uploadResult.PublicId;

        //     var photo = _mapper.Map<Photo>(photoForCreationDTO);

        //     if(!userFromRepo.Photos.Any(u => u.IsMain))
        //         photo.IsMain = true;

        //     userFromRepo.Photos.Add(photo);

            
        //     if (await _repo.SaveAll())
        //     {
        //         var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photo);
        //         return CreatedAtRoute("GetPhoto", new { userId = userId, id = photo.Id},
        //         photoToReturn);
        //     }

        //     return BadRequest("Could not add the photo");
        // }

    }
}