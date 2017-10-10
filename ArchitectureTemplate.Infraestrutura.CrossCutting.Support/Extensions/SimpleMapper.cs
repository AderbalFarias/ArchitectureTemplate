using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions
{
    public static class SimpleMappper
    {
        public static IEnumerable<T> CastAll<T>(this IEnumerable<object> objCast) where T : new()
        {
            return objCast.Select(s => s.Cast<T>());
        }

        public static T Cast<T>(this object objCast) where T : new()
        {
            if (objCast == null)
                return new T();

            Type target = typeof(T);
            var instance = Activator.CreateInstance(target, false);

            var memberInfos = target
                .GetMembers()
                .Where(w => w.MemberType == MemberTypes.Property)
                .Select(s => s)
                .ToList();

            IList<MemberInfo> members = memberInfos
                .Where(memberInfo => memberInfos.Select(c => c.Name).Contains(memberInfo.Name))
                .ToList();

            var membersName = objCast
                .GetType()
                .GetMembers()
                .Where(memberInfo => memberInfos.Select(c => c.Name).Contains(memberInfo.Name))
                .Select(s => s.Name)
                .ToList();

            object value;
            PropertyInfo propertyInfo;
            foreach (var member in members.Where(member => membersName.Contains(member.Name)))
            {
                propertyInfo = typeof(T).GetProperty(member.Name);
                value = objCast.GetType()
                    .GetProperty(member.Name)
                    ?.GetValue(objCast, null);

                if (propertyInfo != null)
                    propertyInfo.SetValue(instance, value, null);
            }

            return (T)instance;
        }
    }
}
