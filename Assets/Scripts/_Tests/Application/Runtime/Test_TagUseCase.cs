using System;
using System.Collections;
using NUnit.Framework;
using Moq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;
using Project.Application.UseCase;

namespace Tests.Application {

    public sealed class Test_TagUseCase {

        private Mock<ITagRepository> _mockRepository;
        private TagUseCase _useCase;

        [UnityTest]
        public IEnumerator EnumeratorTest() {
            yield return null;
            Assert.That(0, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator EnumeratorTest2() {
            yield return new WaitForSeconds(1);
            Assert.That(0, Is.EqualTo(0));
        }

        [SetUp]
        public void Setup() {
            Debug.Log("Setup");

            _mockRepository = new Mock<ITagRepository>();
            Assert.IsNotNull(_mockRepository, "_mockRepository initialization failed.");

            _useCase = new TagUseCase(_mockRepository.Object);
            Assert.IsNotNull(_useCase, "_useCase initialization failed.");
        }


        [UnityTest]
        public IEnumerator CreateTagAsync_ValidName_AddsTag() => UniTask.ToCoroutine(async () => {
            
            var name = "NewTag";

            // モックの設定: name に一致するタグは存在しない（null を返す）
            _mockRepository
                .Setup(repo => repo.FindByNameAsync(name))
                .Returns(UniTask.FromResult<Tag>(null));

            // テスト対象メソッドの呼び出し
            await _useCase.CreateTagAsync(name);

            // モックの AddAsync メソッドが正しく呼び出されたことを検証
            _mockRepository.Verify(
                repo => repo.AddAsync(It.Is<Tag>(t => t.Name == name)),
                Times.Once);
        });
    }
}
