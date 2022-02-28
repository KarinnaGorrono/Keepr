using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _profileRepo;

        public ProfilesService(ProfilesRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }

        internal Profile GetByProfileId(string id)
        {
            Profile Profile = _profileRepo.GetByProfileId(id);
            if (Profile == null)
            {
                throw new Exception("Profile ID incorrect");
            }
            return Profile;
        }
    }
}
