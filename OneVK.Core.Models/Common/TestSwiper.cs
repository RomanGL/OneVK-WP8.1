using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Core.Models.Common
{
    public class TestSwiper : ISwiper
    {
        private readonly List<string> _source = new List<string>
        {
            "http://windows-phone-7.su/uploads/posts/2015-03/1427403390_onevk-windows-phone.jpg",
            "http://cdn.bimmertoday.de/wp-content/uploads/2015/02/BMW-i8-Gelb-Folierung-14.jpg",
            "http://windows-phone-7.su/uploads/posts/2015-12/1448989029_onevk-windows-phone-2.jpg",
            "http://cs.mypleer.com/news/uploads/2013/04/1305549926_17-recept-salat-fruktovyj-s-likerom-1-730x547.jpg",
            "http://img-f.photosight.ru/434/4076692_large.jpeg",
            "http://www.joelmalm.com/wp-content/uploads/2014/11/Main-page-of-the-section-narcissus-photos-narcissus-flower-pictures.jpg"
        };
        private int _index;

        public TestSwiper()
        {
            _index = (new Random(Environment.TickCount)).Next(0, _source.Count);
        }

        public object GetBackward()
        {
            if (_index == 0) return _source[_source.Count - 1];
            return _source[_index - 1];
        }

        public object GetCurrent()
        {
            return _source[_index];
        }

        public object GetForward()
        {
            if (_index == _source.Count - 1) return _source[0];
            return _source[_index + 1];
        }

        public bool GoBackward()
        {
            if (_index == 0) _index = _source.Count - 1;
            else _index -= 1;

            return true;
        }

        public bool GoForward()
        {
            if (_index == _source.Count - 1) _index = 0;
            else _index += 1;

            return true;
        }
    }
}
