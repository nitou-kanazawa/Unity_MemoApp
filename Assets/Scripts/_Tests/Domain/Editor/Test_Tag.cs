using System;
using System.Collections;
using NUnit.Framework;
using Moq;
using UnityEngine;
using UnityEngine.TestTools;
using Project.Domain.Memos.Model;

namespace Test.Domain {

    public class Test_Tag {

        public interface IFunc {
            bool Invoke(int number);
        }

        public class FuncProxy {
            private readonly IFunc _func;
            public FuncProxy(IFunc func) {
                _func = func;
            }
            public bool Invoke(int number) => _func.Invoke(number);
        }

        [Test]
        public void Invoke_ReturnsFalse_IfFuncReturnsFalse() {
            // Arrange
            var mock = new Mock<IFunc>();
            var target = new FuncProxy(mock.Object);

            // Note: Moqの仕様でSetupなしの場合はdefaultを返す (bool Invoke(...) の場合はfalse)
            // FYI: 実際のテストではテストパターンを明確にするために明示しましょう！

            // Act
            bool actual = target.Invoke(3);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Invoke_ReturnsTrue_IfFuncReturnsTrue() {
            // Arrange
            var mock = new Mock<IFunc>();
            var target = new FuncProxy(mock.Object);

            // Note: 引数3を渡されたらtrueを返す
            mock.Setup(m => m.Invoke(3)).Returns(true);

            // Act
            bool actual = target.Invoke(3);

            // Assert
            Assert.That(actual, Is.True);
        }


        [Test]
        public void Test_TagSimplePasses() {
            // 
            var tagId = new TagId(Guid.NewGuid());
            var tag = new Tag(tagId, "TestTag");

            Assert.AreEqual("TestTag", tag.Name);
            Assert.AreEqual(tagId, tag.Id);
        }

        [Test]
        public void CreateTag_WithEmptyName_ThrowsException() {
            var tagId = new TagId(Guid.NewGuid());
            Assert.Throws<InvalidOperationException>(() => new Tag(tagId, ""));
        }


        [Test]
        public void TagClone_CreatesExactCopy() {
            var tagId = new TagId(Guid.NewGuid());
            var tag = new Tag(tagId, "TestTag");

            var clone = tag.Clone();
            Assert.AreEqual(tag.Id, clone.Id);
            Assert.AreEqual(tag.Name, clone.Name);
        }
    }
}
