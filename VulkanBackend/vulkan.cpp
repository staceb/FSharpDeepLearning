#include <stdexcept>
#include <vulkan/vulkan.h>
#include "vulkan.h"

#ifdef _WIN32
#include <ciso646>
#endif

namespace vulkan
{
	VkInstance instance;

	void init_vulkan ()
	{
		VkApplicationInfo app =
		{
			VK_STRUCTURE_TYPE_APPLICATION_INFO,
			nullptr,
			// app
			"",
			VK_MAKE_VERSION (1, 0, 0),
			// engine
			"",
			VK_MAKE_VERSION (1, 0, 0),
			// api
			VK_MAKE_VERSION (1, 0, 65)
		};

		VkInstanceCreateInfo info =
		{
			VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
			nullptr,
			0,
			&app,
			// layer
			0,
			nullptr,
			// extensions
			0,
			nullptr
		};

		if (not vkCreateInstance (&info, nullptr, &instance))
			throw std::runtime_error ("vkCreateInstance failed.");
	}

	void term_vulkan ()
	{
		vkDestroyInstance (instance, nullptr);
	}
}
