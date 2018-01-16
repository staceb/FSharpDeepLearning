
open System.Runtime.InteropServices

module Vulkan =
    [<DllImport ("VulkanBackend.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern void init_vulkan ();

    let InitVulkan () =
        init_vulkan ()

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
