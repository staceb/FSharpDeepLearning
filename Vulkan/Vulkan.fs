
open System.Runtime.InteropServices

module Vulkan =
    [<DllImport ("VulkanBackend.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern void init_vulkan ();
    
    [<DllImport ("VulkanBackend.dll", CallingConvention = CallingConvention.Cdecl)>]
    extern void term_vulkan ();

    let InitVulkan =
        init_vulkan ()

    let TermVulkan =
        term_vulkan ()

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
