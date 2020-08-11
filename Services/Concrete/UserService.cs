using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using Core.Models;
using DbReader.Interfaces;
using Microsoft.Extensions.Options;
using Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Services.Concrete
{
    internal class UserService : IUserService
    {

        private readonly IDbReader _dbReader;
        private readonly IMapper _mapper;
        private readonly UserOptions _userOptions;

        private bool _isDisposed = false;

        public UserService(IDbReader dbReader, IOptions<UserOptions> userOptions, IMapper mapper)
        {
            if (dbReader is null)
            {
                throw new System.ArgumentNullException(nameof(dbReader));
            }

            if (userOptions is null)
            {
                throw new System.ArgumentNullException(nameof(userOptions));
            }

            _dbReader = dbReader;
            _mapper = mapper;
            _userOptions = userOptions.Value;
        }

        public UserDetails AddUser(UserDetails userUpdate, string domain)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { nameof(userUpdate.Firstname), userUpdate.Firstname },
                { nameof(userUpdate.Lastname), userUpdate.Lastname },
                { nameof(userUpdate.City), userUpdate.City },
                { nameof(userUpdate.Country), userUpdate.Country },
                { nameof(userUpdate.Company), userUpdate.Country },
                { nameof(domain), domain },
                { nameof(userUpdate.Address), userUpdate.Address },
            };

            if(userUpdate.Phones != null && userUpdate.Phones.Count > 0)
            {
                parameters.Add("PhoneNumbers", JsonSerializer.Serialize(userUpdate.Phones));
            }

            User user =_dbReader.Exec<User>(_userOptions.AddUser, parameters).SingleOrDefault();


            UserDetails userDetails = _mapper.Map<UserDetails>(user);


            return userDetails;

        }

        public UserDetails GetUser(int userId, string domain)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { nameof(userId), userId.ToString() },
                { nameof(domain), domain }
            };

            User user = _dbReader.Exec<User>(_userOptions.GetUser, parameters).SingleOrDefault();
            IEnumerable<Phone> phones = _dbReader.Exec<Phone>(_userOptions.GetUsersPhones, parameters);

            UserDetails userDetails = _mapper.Map<UserDetails>(user);

            userDetails.Phones = phones.ToList();

            return userDetails;
        }

        public IEnumerable<UserShortInfo> GetUsers(string domain)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { nameof(domain), domain }
            };

            IEnumerable<User> users = _dbReader.Exec<User>(_userOptions.GetUsers, parameters);

            IEnumerable<UserShortInfo> userShortInfos = _mapper.Map<IEnumerable<UserShortInfo>>(users);

            return userShortInfos;
        }

        public UserDetails UpdateUser(UserDetails userUpdate, string domain)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { nameof(userUpdate.UserId), userUpdate.UserId.ToString() },
                { nameof(userUpdate.Firstname), userUpdate.Firstname },
                { nameof(userUpdate.Lastname), userUpdate.Lastname },
                { nameof(userUpdate.City), userUpdate.City },
                { nameof(userUpdate.Country), userUpdate.Country },
                { nameof(userUpdate.Address), userUpdate.Address },
                { nameof(domain), domain }
            };

            User user = _dbReader.Exec<User>(_userOptions.UpdateUser, parameters).Single();
            IEnumerable<Phone> phones = _dbReader.Exec<Phone>(_userOptions.GetUsersPhones, parameters);

            UserDetails userDetails = _mapper.Map<UserDetails>(user);

            userDetails.Phones = phones.ToList();

            return userDetails;
        }


        public void DeleteUser(int userid, string domain)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { nameof(userid), userid.ToString() },
                { nameof(domain), domain }
            };

            _dbReader.Exec<User>(_userOptions.RemoveUser, parameters);
        }

        ~UserService()
        {
            if (!_isDisposed)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _dbReader.Dispose();

            GC.SuppressFinalize(this);

            _isDisposed = true;
        }
    }
}
