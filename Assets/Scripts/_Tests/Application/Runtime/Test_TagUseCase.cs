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

            // ���b�N�̐ݒ�: name �Ɉ�v����^�O�͑��݂��Ȃ��inull ��Ԃ��j
            _mockRepository
                .Setup(repo => repo.FindByNameAsync(name))
                .Returns(UniTask.FromResult<Tag>(null));

            // �e�X�g�Ώۃ��\�b�h�̌Ăяo��
            await _useCase.CreateTagAsync(name);

            // ���b�N�� AddAsync ���\�b�h���������Ăяo���ꂽ���Ƃ�����
            _mockRepository.Verify(
                repo => repo.AddAsync(It.Is<Tag>(t => t.Name == name)),
                Times.Once);
        });
    }
}
