﻿using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет размер файла, выраженный в байтах, килобайтах, мегабайтах, 
    /// гигабайтах, терабайтах, петабайтах и эксабайтах.
    /// </summary>
    [DataContract]
    public struct FileSize : IEquatable<FileSize>, IComparable, IComparable<FileSize>
    {        
        private ulong _bytes, _kilobytes;
        private double _megabytes, _gigabytes, _terabytes, _petabytes, _exabytes;

        /// <summary>
        /// Возвращает размер в байтах.
        /// </summary>
        [DataMember(Name = "bytes")]
        [JsonProperty("bytes")]
        public ulong Bytes 
        { 
            get { return _bytes; } 
            private set
            {
                _bytes = value;
                Calculate();
            }
        }

        /// <summary>
        /// Возвращает размер в килобайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public ulong Kilobytes { get { return _kilobytes; } }

        /// <summary>
        /// Возвращает размер в мегабайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public double Megabytes { get { return _megabytes; } }

        /// <summary>
        /// Возвращает размер в гигабайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public double Gigabytes { get { return _gigabytes; } }

        /// <summary>
        /// Возвращает размер в терабайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public double Terabytes { get { return _terabytes; } }

        /// <summary>
        /// Возвращает размер в петабайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public double Petabytes { get { return _petabytes; } }

        /// <summary>
        /// Возвращает размер в эксабайтах.
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public double Exabytes { get { return _exabytes; } }

        #region Публичные методы

        #region Статичные методы
        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа байт.
        /// </summary>
        /// <param name="value">Число байт.</param>
        public static FileSize FromBytes(ulong value)
        {
            return new FileSize { Bytes = value };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа килобайт.
        /// </summary>
        /// <param name="value">Число килобайт.</param>
        public static FileSize FromKilobytes(ulong value)
        {
            return new FileSize { Bytes = value * 1024 };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа мегабайт.
        /// </summary>
        /// <param name="value">Число мегабайт.</param>
        public static FileSize FromMegabytes(double value)
        {
            return new FileSize { Bytes = (ulong)(value * 1048576) };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа гигабайт.
        /// </summary>
        /// <param name="value">Число гигабайт.</param>
        public static FileSize FromGigabytes(double value)
        {
            return new FileSize { Bytes = (ulong)(value * 1073741824) };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа терабайт.
        /// </summary>
        /// <param name="value">Число терабайт.</param>
        public static FileSize FromTerabytes(double value)
        {
            return new FileSize { Bytes = (ulong)(value * 1099511627776) };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа петабайт.
        /// </summary>
        /// <param name="value">Число петабайт.</param>
        public static FileSize FromPetabytes(double value)
        {
            return new FileSize { Bytes = (ulong)(value * 1125899906842624) };
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/> из заданного числа эксабайт.
        /// </summary>
        /// <param name="value">Число эксабайт.</param>
        public static FileSize FromExabytes(double value)
        {
            return new FileSize { Bytes = (ulong)(value * 1152921504606846976) };
        }
        #endregion

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное 
        /// число байт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число байт, которое требуется добавить. 
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddBytes(ulong value)
        {
            return FileSize.FromBytes(Bytes += value);
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число килобайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число килобайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddKilobytes(ulong value)
        {
            return AddBytes(Bytes + value * 1024);
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число мегабайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число полных или неполных мегабайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddMegabytes(double value)
        {
            return AddBytes(Bytes + (ulong)(value * 1048576));
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число гигабайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число полных или неполных гигабайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddGigabytes(double value)
        {
            return AddBytes(Bytes + (ulong)(value * 1073741824));
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число терабайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число полных или неполных терабайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddTerabytes(double value)
        {
            return AddBytes(Bytes + (ulong)(value * 1099511627776));
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число петабайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число полных или неполных петабайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddPetabytes(double value)
        {
            return AddBytes(Bytes + (ulong)(value * 1125899906842624));
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="FileSize"/>, добавляющий заданное
        /// число эксабайт к значению данного экземпляра.
        /// </summary>
        /// <param name="value">Число полных или неполных эксабайт, которое требуется добавить.
        /// Параметр может быть положительным или отрицательным.</param>
        public FileSize AddExabytes(double value)
        {
            return AddBytes(Bytes + (ulong)(value * 1152921504606846976));
        }
        #endregion

        #region Операторы
        /// <summary>
        /// Складывает две структуры <see cref="FileSize"/>, возвращая новый результирующий экземпляр.
        /// </summary>
        /// <param name="fisrt">Первое слагаемое.</param>
        /// <param name="second">Второе слагаемое.</param>
        public static FileSize operator +(FileSize fisrt, FileSize second)
        {
            return FileSize.FromBytes(fisrt.Bytes + second.Bytes);
        }

        /// <summary>
        /// Вычитает указанное значение <see cref="FileSize"/> из другой структуры <see cref="FileSize"/>,
        /// возвращая новый результирующий экземпляр.
        /// </summary>
        /// <param name="first">Уменьшаемое значение.</param>
        /// <param name="second">Вычитаемое значение.</param>
        public static FileSize operator -(FileSize first, FileSize second)
        {
            ulong result = first.Bytes - second.Bytes;
            return result < 0 ? FileSize.FromBytes(0) : FileSize.FromBytes(result);
        }

        /// <summary>
        /// Определяет, равны ли два заданных экземпляра <see cref="FileSize"/>.
        /// </summary>
        /// <param name="first">Первый сравниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator ==(FileSize first, FileSize second)
        {
            return first.Bytes == second.Bytes;
        }

        /// <summary>
        /// Определяет, являются ли два заданных экземпляра <see cref="FileSize"/> неравными.
        /// </summary>
        /// <param name="fisrt">Первый сравниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator !=(FileSize fisrt, FileSize second)
        {
            return fisrt.Bytes != second.Bytes;
        }

        /// <summary>
        /// Определяет, является ли первый экземпляр <see cref="FileSize"/> больше второго.
        /// </summary>
        /// <param name="firts">Первый сраниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator >(FileSize firts, FileSize second)
        {
            return firts.Bytes > second.Bytes;
        }

        /// <summary>
        /// Определяет, является ли второй экземпляр <see cref="FileSize"/> больше первого.
        /// </summary>
        /// <param name="firts">Первый сраниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator <(FileSize firts, FileSize second)
        {
            return firts.Bytes < second.Bytes;
        }

        /// <summary>
        /// Определяет, является ли первый экземпляр <see cref="FileSize"/> большим либо равным второму.
        /// </summary>
        /// <param name="firts">Первый сраниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator >=(FileSize firts, FileSize second)
        {
            return firts.Bytes >= second.Bytes;
        }

        /// <summary>
        /// Определяет, является ли первый экземпляр <see cref="FileSize"/> меньшим либо равным второму.
        /// </summary>
        /// <param name="firts">Первый сраниваемый объект.</param>
        /// <param name="second">Второй сравниваемый объект.</param>
        public static bool operator <=(FileSize firts, FileSize second)
        {
            return firts.Bytes <= second.Bytes;
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Пересчитывает все значения.
        /// </summary>
        private void Calculate()
        {
            _kilobytes = _bytes / 1024;
            _megabytes = _bytes / 1048576D;
            _gigabytes = _bytes / 1073741824D;
            _terabytes = _bytes / 1099511627776D;
            _petabytes = _bytes / 1125899906842624D;
            _exabytes = _bytes / 1152921504606846976D;
        }
        #endregion

        /// <summary>
        /// Сравнивает объект с текущим.
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим.</param>
        public override bool Equals(object obj)
        {
            if (obj is FileSize == false) return false;
            return Equals((FileSize)obj);
        }

        /// <summary>
        /// Указывает, равен ли текущий объект <see cref="FileSize"/> другому объекту <see cref="FileSize"/>.
        /// </summary>
        /// <param name="other">Объект <see cref="FileSize"/>, с которым требуется сравнить текущий.</param>
        public bool Equals(FileSize other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнивает текущий объект <see cref="FileSize"/> с переданным на размер.
        /// </summary>
        /// <param name="other">Объект <see cref="FileSize"/>, с которым требуется сравнить текущий.</param>
        public int CompareTo(FileSize other)
        {
            if (this == other) return 0;
            else return this < other ? -1 : 1;
        }

        /// <summary>
        /// Сравнивает текущий объект <see cref="FileSize"/> с переданным на размер.
        /// </summary>
        /// <param name="other">Объект, с которым требуется сравнить текущий.</param>
        public int CompareTo(object other)
        {
            if (other == null) return 1;
            if (!(other is FileSize))
                throw new ArgumentException("Параметр должен иметь тип FileSize.");

            if (this == (FileSize)other) return 0;
            else return this < (FileSize)other ? -1 : 1;
        }

        /// <summary>
        /// Возвращает хэш-код объекта.
        /// </summary>
        public override int GetHashCode()
        {
            return Bytes.ToString().GetHashCode();
        }
    }
}
