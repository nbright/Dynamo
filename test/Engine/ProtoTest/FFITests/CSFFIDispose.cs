using System;
using System.Collections.Generic;
using System.Linq;
using FFITarget;
using NUnit.Framework;
using ProtoCore.AST.AssociativeAST;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Mirror;
using ProtoScript.Runners;
using ProtoTest.TD;
using ProtoTestFx.TD;
namespace ProtoFFITests
{
    class CSFFIDispose : FFITestSetup
    {
        readonly TestFrameWork thisTest = new TestFrameWork();
        private ILiveRunner astLiveRunner = null;


        [SetUp]
        public void Setup()
        {
            DisposeTracer.DisposeCount = 0;
            AbstractDerivedDisposeTracer2.DisposeCount = 0;
            astLiveRunner = new LiveRunner();
        }


        [Test]
        public void Dispose01_NoFunctionCall()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectStillInScope("cf1", 0);
        }

        [Test]
        public void Dispose02_FunctionNonArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose03_FunctionReplication()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose04_FunctionArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose05_StaticFunctionNonArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose06_StaticFunctionReplication()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose07_StaticFunctionNonArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose08_MemFunctionNonArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose09_MemFunctionReplication()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose10_MemFunctionArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose11_ReplicationNonArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose12_ReplicationReplication()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose13_ReplicationArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose14_GlobalFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose15_GlobalFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose16_GlobalFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose17_StaticFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose18_StaticFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose19_StaticFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose20_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectStillInScope("pt1", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose21_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose22_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose23_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose24_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose25_MemberFunctionTwoArguments()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose26_GlobalFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose27_GlobalFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose28_GlobalFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose29_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose30_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("arc1");
            thisTest.VerifyFFIObjectOutOfScope("arc2");
            thisTest.VerifyFFIObjectOutOfScope("arc3");
            thisTest.VerifyReferenceCount("arcarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose31_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose32_StaticFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose33_StaticFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose34_StaticFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose35_StaticFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose36_StaticFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose37_MemberFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose38_MemberFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose39_GlobalFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose40_GlobalFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectStillInScope("pt2", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt3");
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose41_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose42_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("arc1");
            thisTest.VerifyFFIObjectOutOfScope("arc2");
            thisTest.VerifyFFIObjectOutOfScope("arc3");
            thisTest.VerifyReferenceCount("arcarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose43_MemberFunctionReturnArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose44_MemberFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose45_MemberFunctionReturnObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose46_GlobalFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose47_GlobalFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose48_GlobalFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
        }

        [Test]
        public void Dispose49_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose50_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("arc1");
            thisTest.VerifyFFIObjectOutOfScope("arc2");
            thisTest.VerifyFFIObjectOutOfScope("arc3");
            thisTest.VerifyReferenceCount("arcarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose51_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose52_StaticFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose53_StaticFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose54_StaticFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose55_StaticFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose56_StaticFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose57_MemberFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose58_MemberFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose59_GlobalFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose60_GlobalFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose61_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose62_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("arc1");
            thisTest.VerifyFFIObjectOutOfScope("arc2");
            thisTest.VerifyFFIObjectOutOfScope("arc3");
            thisTest.VerifyReferenceCount("arcarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose63_MemberFunctionReturnNewArray()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose64_MemberFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }

        [Test]
        public void Dispose65_MemberFunctionReturnNewObject()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("pt1");
            thisTest.VerifyFFIObjectOutOfScope("pt2");
            thisTest.VerifyFFIObjectStillInScope("pt3", 0);
            thisTest.VerifyReferenceCount("ptarr", 0);
            thisTest.VerifyFFIObjectOutOfScope("vec1");
            thisTest.VerifyFFIObjectOutOfScope("vec2");
            thisTest.VerifyFFIObjectOutOfScope("vec3");
            thisTest.VerifyReferenceCount("vecarr", 0);
            thisTest.VerifyReferenceCount("test", 0);
            thisTest.VerifyReferenceCount("a1", 0);
            thisTest.VerifyReferenceCount("a2", 0);
            thisTest.VerifyReferenceCount("a3", 0);
            thisTest.VerifyReferenceCount("as", 0);
        }


        [Test]
        public void Dispose67_PointInScope()
        {
            String code =
            @"   
            object[] a = new object[] { 0.0, 0.0, 0.0 };
            ValidationData[] data = { new ValidationData { ValueName = "carr", ExpectedValue = a, BlockIndex = 0 } };
            ExecuteAndVerify(code, data);
        }


        [Test]
        public void Dispose_FFITarget()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("x");
            thisTest.VerifyReferenceCount("x", 0);
            thisTest.Verify("s1", 1);
        }

        [Test]
        public void Dispose_FFITarget_Inherited()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("x");
            thisTest.VerifyReferenceCount("x", 0);
            thisTest.Verify("s1", 1);
        }

        [Test]
        public void Dispose_FFITarget_Overridden()
        {
            String code =
            @"              
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyFFIObjectOutOfScope("x");
            thisTest.VerifyReferenceCount("x", 0);
            thisTest.Verify("s1", 1);
        }


        [Test]
        [Category("Trace")]
        public void InheritedLiveRunnerDispose()
        {
            string setupCode =
            @"import(""FFITarget.dll""); 
x = AbstractDerivedDisposeTracer2.AbstractDerivedDisposeTracer2(); 
s1 = AbstractDerivedDisposeTracer2.DisposeCount;
";

            // Create 2 CBNs

            List<Subtree> added = new List<Subtree>();


            // Simulate a new new CBN
            Guid guid1 = System.Guid.NewGuid();
            added.Add(CreateSubTreeFromCode(guid1, setupCode));

            var syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

            AssertValue("s1", 0);


            // Simulate a new new CBN
            Guid guid2 = System.Guid.NewGuid();
            added = new List<Subtree>();
            added.Add(CreateSubTreeFromCode(guid2,
                "x = null;" +
                "s2 = AbstractDerivedDisposeTracer2.DisposeCount; "));


            syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

            AssertValue("s2", 1);
        }

        [Test]
        [Category("Trace")]
        public void IntermediatePatch()
        {
            string setupCode =
            @"import(""FFITarget.dll""); 
x = DerivedDisposeTracer.DerivedDisposeTracer(); 
";

            // Create 2 CBNs

            List<Subtree> added = new List<Subtree>();


            // Simulate a new new CBN
            Guid guid1 = System.Guid.NewGuid();
            added.Add(CreateSubTreeFromCode(guid1, setupCode));

            var syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);


            // Simulate a new new CBN
            Guid guid2 = System.Guid.NewGuid();
            added = new List<Subtree>();
            added.Add(CreateSubTreeFromCode(guid2,
                "x = null;" ));


            syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

        }

        [Test]
        public void CleanupAfterPropertyAccess()
        {
            String code =
            @"import(""FFITarget.dll""); 
x = AbstractDerivedDisposeTracer2.AbstractDerivedDisposeTracer2(1);
so = x.I;
s1 = AbstractDerivedDisposeTracer2.DisposeCount;
x = null;
s1 = AbstractDerivedDisposeTracer2.DisposeCount;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("s1", 1);
        }

        [Test]
        [Category("Trace")]
        public void CleanupAfterPropertyAccessLR()
        {
            string setupCode =
            @"import(""FFITarget.dll""); 
x = AbstractDerivedDisposeTracer2.AbstractDerivedDisposeTracer2(1);
so = x.I;
s1 = AbstractDerivedDisposeTracer2.DisposeCount;
";

            // Create 2 CBNs

            List<Subtree> added = new List<Subtree>();


            // Simulate a new new CBN
            Guid guid1 = System.Guid.NewGuid();
            added.Add(CreateSubTreeFromCode(guid1, setupCode));

            var syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

            AssertValue("s1", 0);


            // Simulate a new new CBN
            Guid guid2 = System.Guid.NewGuid();
            added = new List<Subtree>();
            added.Add(CreateSubTreeFromCode(guid2,
                "x = null;" +
                "s2 = AbstractDerivedDisposeTracer2.DisposeCount; "));


            syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

            AssertValue("s2", 1);
        }


        //MOVE THIS TEST
        [Test]
        [Category("Trace")]
        public void InheritenceLiveRunner()
        {
            string setupCode =
            @"import(""FFITarget.dll"");
";

            // Create 2 CBNs

            List<Subtree> added = new List<Subtree>();


            // Simulate a new new CBN
            Guid guid1 = System.Guid.NewGuid();
            added.Add(CreateSubTreeFromCode(guid1, setupCode));

            var syncData = new GraphSyncData(null, added, null);
            astLiveRunner.UpdateGraph(syncData);

            AssertValue("oy", 4);


        }


        //Migrate this code into the test framework
        private Subtree CreateSubTreeFromCode(Guid guid, string code)
        {
            CodeBlockNode commentCode;
            var cbn = GraphToDSCompiler.GraphUtilities.Parse(code, out commentCode) as CodeBlockNode;
            var subtree = null == cbn ? new Subtree(null, guid) : new Subtree(cbn.Body, guid);
            return subtree;
        }

        private void AssertValue(string varname, object value)
        {
            var mirror = astLiveRunner.InspectNodeValue(varname);
            MirrorData data = mirror.GetData();
            object svValue = data.Data;
            if (value is double)
            {
                Assert.AreEqual((double)svValue, Convert.ToDouble(value));
            }
            else if (value is int)
            {
                Assert.AreEqual((Int64)svValue, Convert.ToInt64(value));
            }
            else if (value is bool)
            {
                Assert.AreEqual((bool)svValue, Convert.ToBoolean(value));
            }
            else if (value is IEnumerable<int>)
            {
                Assert.IsTrue(data.IsCollection);
                var values = (value as IEnumerable<int>).ToList().Select(v => (object)v).ToList();
                Assert.IsTrue(mirror.GetUtils().CompareArrays(varname, values, typeof(Int64)));
            }
            else if (value is IEnumerable<double>)
            {
                Assert.IsTrue(data.IsCollection);
                var values = (value as IEnumerable<double>).ToList().Select(v => (object)v).ToList();
                Assert.IsTrue(mirror.GetUtils().CompareArrays(varname, values, typeof(double)));
            }
        }



    }
}