using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Helpers;
using OneVK.Model.Profile;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Сервис для работы с пользователями ВКонтакте.
    /// </summary>
    public sealed class VKUserV1Service
    {
        /// <summary>
        /// Возвращает коллекцию базовых профилей запрошенных пользователей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userIDs">Коллекция идентификаторов пользователей,
        /// профили которых требуется получить.</param>
        /// <param name="nameCase">Падеж имени пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetBaseUsers(Action<VKResponse<List<VKProfileBase>>> callback, 
            List<long> userIDs = null, VKUserNameCase nameCase = VKUserNameCase.nom)
        {
            if (userIDs != null && (userIDs.Count == 0 || userIDs.Count() > 100))
                throw new ArgumentOutOfRangeException("userIDs",
                    "Коллекция идентификаторов должна содержать от одного до ста элементов. " +
                    "Если вам требуется получить профиль текущего пользователя, используйте null.");

            var parameters = new Dictionary<string, string>();
            
            parameters["fields"] = "status,online,online_mobile,photo_50,photo_100,photo_200";
            if (nameCase != VKUserNameCase.nom) parameters["name_case"] = nameCase.ToString();
            if (userIDs != null)
            {
                if (userIDs.Count == 1) parameters["user_ids"] = userIDs.First().ToString();
                else parameters["user_ids"] = String.Join(",", userIDs);
            }            

            VKHelper.GetResponse<List<VKProfileBase>>(VKMethodsConstants.UsersGet, parameters, callback);
        }

        /// <summary>
        /// Возвращает коллекцию базовых профилей запрошенных пользователей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userIDs">Коллекция идентификаторов пользователей,
        /// профили которых требуется получить.</param>
        /// <param name="nameCase">Падеж имени пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetExtendedUsers(Action<VKResponse<List<VKProfileExtended>>> callback,
            List<long> userIDs = null, List<VKUserFields> fields = null, VKUserNameCase nameCase = VKUserNameCase.nom)
        {
            if (userIDs != null && (userIDs.Count == 0 || userIDs.Count() > 100))
                throw new ArgumentOutOfRangeException("userIDs",
                    "Коллекция идентификаторов должна содержать от одного до ста элементов. " +
                    "Если вам требуется получить профиль текущего пользователя, используйте null.");
            if (fields != null && fields.Count() == 0)
                throw new ArgumentOutOfRangeException("fields",
                    "Коллекция полей должна содержать как минимум один элемент. " +
                    "Если вам требуется получить все данные, используйте null.");

            var parameters = new Dictionary<string, string>();
                        
            if (nameCase != VKUserNameCase.nom) parameters["name_case"] = nameCase.ToString();
            if (userIDs != null)
            {
                if (userIDs.Count == 1) parameters["user_ids"] = userIDs.First().ToString();
                else parameters["user_ids"] = String.Join(",", userIDs);
            }            
            if (fields == null) parameters["fields"] = "sex,bdate,city,country,photo_50,photo_100"
                + ",photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig,online,online_mobile"
                + ",has_mobile,site,education,universities,schools,can_post,can_see_all_posts"
                + ",can_see_audio,can_write_private_message,status,relation,occupation,activities"
                + ",interests,music,movies,tv,books,games,about,quotes";
            else
            {
                var builder = new StringBuilder();
                for (int i = 0; i < fields.Count; i++)
                    builder.Append(fields[i] + ",");
                parameters["fields"] = builder.ToString();
            }

            VKHelper.GetResponse<List<VKProfileExtended>>(VKMethodsConstants.UsersGet, parameters, callback);
        }

        /// <summary>
        /// Выполняет поиск среди пользователей ВКонтакте.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="q">Строка запроса.</param>
        /// <param name="sortMode">Метод сортировки результатов.
        /// По популярности и по дате регистрации.</param>
        /// <param name="count">Количество результатов, которое требуется вернуть.
        /// Не более 1000.</param>
        /// <param name="offset">Смещение относительно начала коллекции результатов.
        /// Не более 1000.</param>
        /// <param name="cityID">Идентификатор города.</param>
        /// <param name="countryID">Идентификатор страны.</param>
        /// <param name="homeTown">Название родного города.</param>
        /// <param name="universityCountryID">Идентификатор страны, в которой находится университет,
        /// в котором учился пользователь.</param>
        /// <param name="universityID">Идентификатор университета, в котором учился пользователь.</param>
        /// <param name="universityYear">Год окончания университета.</param>
        /// <param name="universityFacultyID">Идентификатор факультета, в котором
        /// учился польхователь.</param>
        /// <param name="universityChairID">Идентификатор кафедры, на которой учился пользователь.</param>
        /// <param name="sex">Пол пользователя.</param>
        /// <param name="relation">Семейное положение пользователя.</param>
        /// <param name="ageFrom">Выполнять поиск по пользователям с этого возраста.</param>
        /// <param name="ageTo">Выполнять поиск по пользователям не старше этого вораста.</param>
        /// <param name="birthDay">День рождения.</param>
        /// <param name="birthMonth">Месяц рождения.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="online">Находится ли польователь в сети.</param>
        /// <param name="hasPhoto">Имеется ли фотография в профиле.</param>
        /// <param name="schoolCountryID">Идентификатор страны, в которой находится школа,
        /// в которой учился пользователь.</param>
        /// <param name="schoolCityID">Идентификатор города, в котором находится школа.</param>
        /// <param name="schoolClass">Номер класса.</param>
        /// <param name="schoolID">Идентификатор школы.</param>
        /// <param name="schoolYear">Год окончания школы.</param>
        /// <param name="religion">Мировоззрение.</param>
        /// <param name="interests">Интересы.</param>
        /// <param name="companyName">Название компании, в которой работает пользователь.</param>
        /// <param name="position">Должность.</param>
        /// <param name="groupID">Идентификатор группы, по пользователям которой требуется искать.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void SearchUsers(Action<VKResponse<VKCountedItemsObject<VKProfileBase>>> callback,
            string q, VKMediaSearchSortMode sortMode = VKMediaSearchSortMode.ByPopularity, int count = 20, 
            int offset = 0, int cityID = 0, int countryID = 0, string homeTown = "",
            int universityCountryID = 0, int universityID = 0, int universityYear = 0,
            int universityFacultyID = 0, int universityChairID = 0, VKUserSex sex = VKUserSex.Unknown,
            VKRelation relation = VKRelation.Unknown, int ageFrom = 0, int ageTo = 0,
            int birthDay = 0, int birthMonth = 0, int birthYear = 0, VKBoolean online = VKBoolean.False,
            VKBoolean hasPhoto = VKBoolean.False, int schoolCountryID = 0, int schoolCityID = 0,
            int schoolClass = 0, int schoolID = 0, int schoolYear = 0, string religion = "",
            string interests = "", string companyName = "", string position = "", long groupID = 0)
        {
            if (count <= 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", 
                    "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 1000)
                throw new ArgumentOutOfRangeException("offset", 
                    "Смещение должно быть не менее нуля, но не более 1000.");
            if (String.IsNullOrWhiteSpace(q))
                throw new ArgumentException("q", 
                    "Строка запроса не должна быть пустой.");
            if (sortMode == VKMediaSearchSortMode.ByDuration)
                throw new ArgumentException("sortMode", 
                    "Для коллекции пользователей невозможен метод сортировки по длительности.");
            if (cityID < 0 || countryID < 0 || universityCountryID < 0 || universityID < 0 ||
                universityYear < 0 || universityFacultyID < 0 || universityChairID < 0 ||
                ageFrom < 0 || ageTo < 0 || birthDay < 0 || birthMonth < 0 || birthYear < 0 ||
                schoolCountryID < 0 || schoolCityID < 0 || schoolClass < 0 || schoolID < 0 ||
                schoolYear < 0 || groupID < 0)
                throw new ArgumentOutOfRangeException(null, "Параметр не может быть отрицательным.");

            var parameters = new Dictionary<string, string>();

            parameters["q"] = Uri.EscapeDataString(q);
            parameters["fields"] = "status,online,online_mobile,photo_50,photo_100,photo_200";
            if (count != 20) parameters["count"] = count.ToString();
            if (offset > 0) parameters["offset"] = offset.ToString();
            if (cityID > 0) parameters["city"] = cityID.ToString();
            if (countryID > 0) parameters["country"] = countryID.ToString();            
            if (universityCountryID > 0) parameters["university_country"] = universityCountryID.ToString();
            if (universityID > 0) parameters["university"] = universityID.ToString();
            if (universityYear > 0) parameters["university_year"] = universityYear.ToString();
            if (universityFacultyID > 0) parameters["university_faculty"] = universityFacultyID.ToString();
            if (universityChairID > 0) parameters["university_chair"] = universityChairID.ToString();
            if (ageFrom > 0) parameters["age_from"] = ageFrom.ToString();
            if (ageTo > 0) parameters["age_to"] = ageTo.ToString();
            if (birthDay > 0) parameters["birth_day"] = birthDay.ToString();
            if (birthMonth > 0) parameters["birth_month"] = birthMonth.ToString();
            if (birthYear > 0) parameters["birth_year"] = birthYear.ToString();
            if (schoolCountryID > 0) parameters["school_country"] = schoolCountryID.ToString();
            if (schoolCityID > 0) parameters["school_city"] = schoolCityID.ToString();
            if (schoolClass > 0) parameters["school_class"] = schoolClass.ToString();
            if (schoolID > 0) parameters["school"] = schoolID.ToString();
            if (schoolYear > 0) parameters["school_year"] = schoolYear.ToString();
            if (groupID > 0) parameters["group_id"] = groupID.ToString();
            if (sex != VKUserSex.Unknown) parameters["sex"] = ((byte)sex).ToString();
            if (relation != VKRelation.Unknown) parameters["status"] = ((byte)relation).ToString();
            if (online == VKBoolean.True) parameters["online"] = "1";
            if (hasPhoto == VKBoolean.True) parameters["has_photo"] = "1";
            if (sortMode == VKMediaSearchSortMode.ByDate) parameters["sort"] = "1";
            if (!String.IsNullOrWhiteSpace(homeTown)) parameters["hometown"] = homeTown;
            if (!String.IsNullOrWhiteSpace(religion)) parameters["religion"] = religion;
            if (!String.IsNullOrWhiteSpace(interests)) parameters["interests"] = interests;
            if (!String.IsNullOrWhiteSpace(companyName)) parameters["company"] = companyName;
            if (!String.IsNullOrWhiteSpace(position)) parameters["position"] = position;

            VKHelper.GetResponse<VKCountedItemsObject<VKProfileBase>>(VKMethodsConstants.UsersSearch, 
                parameters, callback);
        }

        /// <summary>
        /// Пожаловаться на пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="type">Тип жалобы.</param>
        /// <param name="comment">Комментарий к жалобе.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ReportUser(Action<VKResponse<VKOperationIsSuccess>> callback,
            long userID, VKBoolean type, string comment = "")
        {
            if (userID <= 0)
                throw new ArgumentOutOfRangeException("userID", 
                    "Идентификатор пользователя должен быть явно указан.");

            var parameters = new Dictionary<string, string>();

            parameters["user_id"] = userID.ToString();
            parameters["type"] = type.ToString();
            if (!String.IsNullOrWhiteSpace(comment)) parameters["comment"] = comment;

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.UserReport, parameters, callback);
        }

        /// <summary>
        /// Получить подписчиков пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="count">Количество пользователей, которое требуется 
        /// вернуть (не более 1000).</param>
        /// <param name="offset">Смещение относительно начала списка 
        /// (не более 1000).</param>
        /// <param name="nameCase">Падеж имени пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetFollowers(Action<VKResponse<VKCountedItemsObject<VKProfileBase>>> callback,
            long userID = 0, int count = 100, int offset = 0, VKUserNameCase nameCase = VKUserNameCase.nom)
        {
            if (count <= 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count",
                    "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 1000)
                throw new ArgumentOutOfRangeException("offset",
                    "Смещение должно быть не менее нуля, но не более 1000.");
            if (userID < 0)
                throw new ArgumentOutOfRangeException("userID",
                    "Идентификатор пользователя не может быть отрицательным.");

            var parameters = new Dictionary<string, string>();

            if (userID > 0) parameters["user_id"] = userID.ToString();
            if (count != 100) parameters["count"] = count.ToString();
            if (offset > 0) parameters["offset"] = offset.ToString();
            if (nameCase != VKUserNameCase.nom) parameters["name_case"] = nameCase.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKProfileBase>>(VKMethodsConstants.UserGetFollowers, 
                parameters, callback);
        }
    }
}
