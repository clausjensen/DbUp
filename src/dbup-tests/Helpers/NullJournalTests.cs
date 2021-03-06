﻿#if !NETCORE
using System;
using System.Data;
using DbUp.Helpers;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DbUp.Tests.Helpers
{
    public class NullJournalTests
    {
        [Fact]
        public void shouldnt_journal_anything_executed()
        {
            var journal = new NullJournal();
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            connection.CreateCommand().Returns(command);

            var upgradeEngine = DeployChanges.To
                .SqlDatabase(() => connection, "Db")
                .WithScript("testscript", "SELECT * FROM BLAH")
                .JournalTo(journal)
                .Build();

            upgradeEngine.PerformUpgrade();

            journal.GetExecutedScripts().ShouldBeEmpty();
        }
    }
}
#endif